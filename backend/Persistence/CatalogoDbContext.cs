using Microsoft.EntityFrameworkCore;
using Sesi.Catalogo.Api.Entities;

namespace Sesi.Catalogo.Api.Persistence;

public sealed class CatalogoDbContext(DbContextOptions<CatalogoDbContext> options) : DbContext(options)
{
    public DbSet<Categoria> Categorias => Set<Categoria>();
    public DbSet<Curso> Cursos => Set<Curso>();
    public DbSet<Modulo> Modulos => Set<Modulo>();
    public DbSet<Aula> Aulas => Set<Aula>();

    protected override void OnModelCreating(ModelBuilder model)
    {
        model.Entity<Categoria>(e =>
        {
            e.ToTable("categorias"); e.HasKey(x => x.Id);
            e.Property(x => x.Codigo).HasMaxLength(60).IsRequired();
            e.Property(x => x.Nome).HasMaxLength(120).IsRequired();
            e.HasIndex(x => x.Codigo).IsUnique();
        });
        model.Entity<Curso>(e =>
        {
            e.ToTable("cursos", t =>
            {
                t.HasCheckConstraint("ck_curso_carga", "\"CargaHoraria\" > 0");
                t.HasCheckConstraint("ck_curso_modalidade", "\"Modalidade\" IN ('online', 'semipresencial')");
                t.HasCheckConstraint("ck_curso_status", "\"Status\" IN ('disponivel', 'em-breve', 'encerrado')");
            });
            e.HasKey(x => x.Id);
            e.Property(x => x.NomeCompleto).HasMaxLength(200).IsRequired();
            e.Property(x => x.NomeBreve).HasMaxLength(100).IsRequired();
            e.Property(x => x.Resumo).HasMaxLength(400).IsRequired();
            e.Property(x => x.Descricao).HasMaxLength(6000).IsRequired();
            e.Property(x => x.PublicoAlvo).HasMaxLength(500).IsRequired();
            e.Property(x => x.Imagem).HasMaxLength(300).IsRequired();
            e.Property(x => x.Modalidade).HasMaxLength(30).IsRequired();
            e.Property(x => x.Status).HasMaxLength(30).IsRequired();
            e.HasOne(x => x.Categoria).WithMany().HasForeignKey(x => x.CategoriaId).OnDelete(DeleteBehavior.Restrict);
            e.HasIndex(x => new { x.CategoriaId, x.Modalidade, x.Status });
            e.HasMany(x => x.Modulos).WithOne().HasForeignKey(x => x.CursoId).OnDelete(DeleteBehavior.Cascade);
        });
        model.Entity<Modulo>(e =>
        {
            e.ToTable("modulos", t => t.HasCheckConstraint("ck_modulo_ordem", "\"Ordem\" > 0"));
            e.HasKey(x => x.Id);
            e.Property(x => x.Titulo).HasMaxLength(200).IsRequired();
            e.Property(x => x.Descricao).HasMaxLength(2000).IsRequired();
            e.HasIndex(x => new { x.CursoId, x.Ordem }).IsUnique();
            e.HasMany(x => x.Aulas).WithOne().HasForeignKey(x => x.ModuloId).OnDelete(DeleteBehavior.Cascade);
        });
        model.Entity<Aula>(e =>
        {
            e.ToTable("aulas", t => t.HasCheckConstraint("ck_aula_ordem", "\"Ordem\" > 0"));
            e.HasKey(x => x.Id);
            e.Property(x => x.Titulo).HasMaxLength(200).IsRequired();
            e.Property(x => x.Descricao).HasMaxLength(2000).IsRequired();
            e.HasIndex(x => new { x.ModuloId, x.Ordem }).IsUnique();
        });
        CatalogoSeed.Configure(model);
    }
}
