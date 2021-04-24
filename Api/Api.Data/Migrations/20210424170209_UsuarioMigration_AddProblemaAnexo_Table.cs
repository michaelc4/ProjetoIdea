using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Api.Data.Migrations
{
    public partial class UsuarioMigration_AddProblemaAnexo_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProblemaAnexo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    DesAnexo = table.Column<string>(type: "LONGTEXT", nullable: true),
                    IndTipoArquivo = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: true),
                    DesNomeOriginal = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true),
                    ProblemaId = table.Column<Guid>(type: "char(36)", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProblemaAnexo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProblemaAnexo_Problema_ProblemaId",
                        column: x => x.ProblemaId,
                        principalTable: "Problema",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProblemaAnexo_ProblemaId",
                table: "ProblemaAnexo",
                column: "ProblemaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProblemaAnexo");
        }
    }
}
