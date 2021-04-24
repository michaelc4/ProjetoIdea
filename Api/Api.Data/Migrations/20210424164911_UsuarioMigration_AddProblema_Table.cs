using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Api.Data.Migrations
{
    public partial class UsuarioMigration_AddProblema_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Problema",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    DesProblema = table.Column<string>(type: "LONGTEXT", nullable: true),
                    IndTipoBeneficio = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: true),
                    IndTipoSolucao = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: true),
                    NumProbabilidadeInvestir = table.Column<int>(type: "int", nullable: false),
                    IndAtivo = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: true),
                    IndAprovado = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: true),
                    UsuarioId = table.Column<Guid>(type: "char(36)", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Problema", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Problema_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Problema_UsuarioId",
                table: "Problema",
                column: "UsuarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Problema");
        }
    }
}
