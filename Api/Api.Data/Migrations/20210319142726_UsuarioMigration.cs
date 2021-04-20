using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Api.Data.Migrations
{
    public partial class UsuarioMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    DesImagem = table.Column<string>(type: "LONGTEXT", nullable: true),
                    DesEmail = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    DesSenha = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    DesTelefone = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    DesEspecialidade = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: true),
                    DesExperiencia = table.Column<string>(type: "longtext", maxLength: 3000, nullable: true),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_DesEmail",
                table: "Usuario",
                column: "DesEmail",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
