using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sesi.Catalogo.Api.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AlignOfficialCourseImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "cursos",
                keyColumn: "Id",
                keyValue: 1,
                column: "Imagem",
                value: "/assets/oficiais/figma/medicao-metalurgia.png");

            migrationBuilder.UpdateData(
                table: "cursos",
                keyColumn: "Id",
                keyValue: 2,
                column: "Imagem",
                value: "/assets/oficiais/figma/industria-textil.jpg");

            migrationBuilder.UpdateData(
                table: "cursos",
                keyColumn: "Id",
                keyValue: 3,
                column: "Imagem",
                value: "/assets/oficiais/figma/controle-producao.jpg");

            migrationBuilder.UpdateData(
                table: "cursos",
                keyColumn: "Id",
                keyValue: 4,
                column: "Imagem",
                value: "/assets/oficiais/figma/soldagem-industrial.jpg");

            migrationBuilder.UpdateData(
                table: "cursos",
                keyColumn: "Id",
                keyValue: 5,
                column: "Imagem",
                value: "/assets/oficiais/figma/laboratorio-quimico.jpg");

            migrationBuilder.UpdateData(
                table: "cursos",
                keyColumn: "Id",
                keyValue: 6,
                column: "Imagem",
                value: "/assets/oficiais/figma/equipamento-industrial.jpg");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "cursos",
                keyColumn: "Id",
                keyValue: 1,
                column: "Imagem",
                value: "/assets/oficiais/banner-ava.png");

            migrationBuilder.UpdateData(
                table: "cursos",
                keyColumn: "Id",
                keyValue: 2,
                column: "Imagem",
                value: "/assets/oficiais/banner-ava.png");

            migrationBuilder.UpdateData(
                table: "cursos",
                keyColumn: "Id",
                keyValue: 3,
                column: "Imagem",
                value: "/assets/oficiais/banner-ava.png");

            migrationBuilder.UpdateData(
                table: "cursos",
                keyColumn: "Id",
                keyValue: 4,
                column: "Imagem",
                value: "/assets/oficiais/banner-ava.png");

            migrationBuilder.UpdateData(
                table: "cursos",
                keyColumn: "Id",
                keyValue: 5,
                column: "Imagem",
                value: "/assets/oficiais/banner-ava.png");

            migrationBuilder.UpdateData(
                table: "cursos",
                keyColumn: "Id",
                keyValue: 6,
                column: "Imagem",
                value: "/assets/oficiais/banner-ava.png");
        }
    }
}
