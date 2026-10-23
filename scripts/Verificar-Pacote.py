"""Audita, compacta e confere o pacote usando apenas a biblioteca padrão Python."""
import argparse
import csv
import hashlib
import io
import json
from pathlib import Path, PurePosixPath
import re
from urllib.parse import unquote, urlsplit
import zipfile


def sha(data):
    return hashlib.sha256(data).hexdigest()


def main():
    parser = argparse.ArgumentParser()
    parser.add_argument('--package', type=Path, required=True)
    parser.add_argument('--zip', type=Path, required=True)
    parser.add_argument('--private-env', type=Path)
    parser.add_argument('--extract', type=Path)
    parser.add_argument('--create', action='store_true')
    args = parser.parse_args()
    package = args.package.resolve()
    files = {p.relative_to(package).as_posix(): p for p in package.rglob('*') if p.is_file()}
    if not files or 'app/wwwroot/index.html' not in files:
        raise ValueError('Pacote sem aplicação/React.')

    # Valores privados são lidos apenas em memória; nunca aparecem no relatório.
    private_values = set()
    if args.private_env:
        for line in args.private_env.read_text(encoding='utf-8-sig').splitlines():
            key, separator, value = line.partition('=')
            if not separator or not re.search(r'DATABASE_URL|TOKEN|SECRET|PASSWORD', key, re.I):
                continue
            value = value.strip().strip('\'"')
            if len(value) > 7:
                private_values.add(value)
            if value.startswith(('postgres://', 'postgresql://')):
                parsed = urlsplit(value)
                if parsed.password:
                    private_values.update((parsed.password, unquote(parsed.password)))
    patterns = [
        rb'-----BEGIN (?:RSA |EC |OPENSSH )?PRIVATE KEY-----',
        rb'gh[pousr]_[A-Za-z0-9]{30,}',
        rb'github_pat_[A-Za-z0-9_]{40,}',
        rb'eyJ[A-Za-z0-9_-]{15,}\.[A-Za-z0-9_-]{15,}\.[A-Za-z0-9_-]{15,}',
    ]
    blocked = []
    for name, path in files.items():
        if name in ('MANIFESTO.csv', 'SHA256SUMS'):
            continue
        parts = PurePosixPath(name).parts
        if (any(part in ('.git', '.neon', 'node_modules', 'bin', 'obj') for part in parts)
                or path.name.startswith(('.env', '.neon'))
                or path.suffix.lower() in ('.pdb', '.log', '.db', '.sqlite', '.pem', '.key', '.pfx', '.tmp')
                or path.name in ('appsettings.Development.json', 'Microsoft.EntityFrameworkCore.Design.dll')):
            blocked.append(name)
        data = path.read_bytes()
        if any(value.encode(encoding) in data for value in private_values for encoding in ('utf-8', 'utf-16-le')):
            blocked.append(name)
        if any(re.search(pattern, data) for pattern in patterns):
            blocked.append(name)
        if name.startswith('app/wwwroot/') and path.suffix == '.js' and re.search(rb'https?://(?:localhost|127\.0\.0\.1)(?::|/|\b)', data):
            blocked.append(name)
        if path.name.startswith('appsettings'):
            config = json.loads(data)
            if any(config.get('ConnectionStrings', {}).values()):
                blocked.append(name)
    if blocked:
        # Não imprime sequer o trecho encontrado ou o valor privado.
        raise ValueError(f'Auditoria bloqueou {len(set(blocked))} arquivo(s); ZIP não autorizado.')

    if args.create:
        if args.zip.exists():
            raise ValueError('ZIP já existe; preserve o artefato anterior.')
        payload = {name: path for name, path in files.items() if name not in ('MANIFESTO.csv', 'SHA256SUMS')}
        with (package / 'MANIFESTO.csv').open('w', newline='', encoding='utf-8') as stream:
            writer = csv.DictWriter(stream, fieldnames=['path', 'bytes', 'sha256'])
            writer.writeheader()
            for name, path in sorted(payload.items()):
                writer.writerow(dict(path=name, bytes=path.stat().st_size, sha256=sha(path.read_bytes())))
        payload['MANIFESTO.csv'] = package / 'MANIFESTO.csv'
        (package / 'SHA256SUMS').write_text(''.join(f'{sha(path.read_bytes())}  {name}\n' for name, path in sorted(payload.items())), encoding='utf-8')
        files = {p.relative_to(package).as_posix(): p for p in package.rglob('*') if p.is_file()}
        with zipfile.ZipFile(args.zip, 'w', compression=zipfile.ZIP_DEFLATED, compresslevel=9) as archive:
            for name, path in sorted(files.items()):
                archive.write(path, name)

    manifest = list(csv.DictReader(io.StringIO((package / 'MANIFESTO.csv').read_text(encoding='utf-8-sig'))))
    if {row['path'] for row in manifest} != set(files) - {'MANIFESTO.csv', 'SHA256SUMS'}:
        raise ValueError('Inventário de conteúdo divergente.')
    for row in manifest:
        data = files[row['path']].read_bytes()
        if len(data) != int(row['bytes']) or sha(data) != row['sha256']:
            raise ValueError('Manifesto divergente.')
    checksum_entries = {}
    for line in (package / 'SHA256SUMS').read_text(encoding='utf-8-sig').splitlines():
        digest, name = line.split('  ', 1)
        checksum_entries[name] = digest
        if sha(files[name].read_bytes()) != digest:
            raise ValueError('Checksum divergente.')
    if set(checksum_entries) != set(files) - {'SHA256SUMS'}:
        raise ValueError('Inventário de checksums divergente.')
    with zipfile.ZipFile(args.zip) as archive:
        names = archive.namelist()
        if len(set(names)) != len(names) or set(names) != set(files):
            raise ValueError('Inventário ZIP divergente.')
        if archive.testzip() is not None:
            raise ValueError('Falha CRC ZIP.')
        for name in names:
            relative = PurePosixPath(name)
            if relative.is_absolute() or '..' in relative.parts or '\\' in name or ':' in name:
                raise ValueError('Caminho inseguro no ZIP.')
            if archive.read(name) != files[name].read_bytes():
                raise ValueError('Conteúdo ZIP divergente.')
        if args.extract:
            if args.extract.exists():
                raise ValueError('Destino de extração já existe.')
            archive.extractall(args.extract)
            if any((args.extract / name).read_bytes() != path.read_bytes() for name, path in files.items()):
                raise ValueError('Extração divergente.')
    digest = sha(args.zip.read_bytes())
    if args.create:
        args.zip.with_suffix(args.zip.suffix + '.sha256').write_text(f'{digest}  {args.zip.name}\n', encoding='utf-8')
    else:
        expected = args.zip.with_suffix(args.zip.suffix + '.sha256').read_text().split()[0]
        if expected != digest:
            raise ValueError('Checksum externo divergente.')
    print(json.dumps(dict(files=len(files), manifest_entries=len(manifest), checksum_entries=len(checksum_entries),
                          zip_bytes=args.zip.stat().st_size, zip_sha256=digest, crc='OK', checksums='OK',
                          extraction='OK' if args.extract else 'não solicitada',
                          security='sem ocorrências nos padrões/valores privados conhecidos; não é auditoria exaustiva',
                          private_values_checked=bool(private_values)), ensure_ascii=False, indent=2))


if __name__ == '__main__':
    main()
