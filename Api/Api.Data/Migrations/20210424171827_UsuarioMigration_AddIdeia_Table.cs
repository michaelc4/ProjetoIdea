using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Api.Data.Migrations
{
    public partial class UsuarioMigration_AddIdeia_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ideia",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    DesIdeia = table.Column<string>(type: "LONGTEXT", nullable: true),
                    DesMotivoInvestir = table.Column<string>(type: "LONGTEXT", nullable: true),
                    IndInteresseCompartilhar = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: true),
                    IndNivelDesenvolvimento = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: true),
                    IndNivelSigilo = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: true),
                    DesComentario = table.Column<string>(type: "LONGTEXT", nullable: true),
                    NumPotencialDisrupcao = table.Column<int>(type: "int", nullable: false),
                    NumPessoasImpactadas = table.Column<int>(type: "int", nullable: false),
                    NumPotencialGanho = table.Column<int>(type: "int", nullable: false),
                    NumValorInvestimento = table.Column<int>(type: "int", nullable: false),
                    NumImpactoAmbiental = table.Column<int>(type: "int", nullable: false),
                    NumPontuacaoGeral = table.Column<int>(type: "int", nullable: false),
                    IndAtivo = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: true),
                    IndAprovado = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: true),
                    UsuarioId = table.Column<Guid>(type: "char(36)", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ideia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ideia_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ideia_UsuarioId",
                table: "Ideia",
                column: "UsuarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ideia");
        }
    }
}
