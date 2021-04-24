using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Api.Data.Migrations
{
    public partial class UsuarioMigration_AddIdeiaAnexo_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IdeiaAnexo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    DesAnexo = table.Column<string>(type: "LONGTEXT", nullable: true),
                    IndTipoArquivo = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: true),
                    DesNomeOriginal = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true),
                    IdeiaId = table.Column<Guid>(type: "char(36)", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdeiaAnexo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IdeiaAnexo_Ideia_IdeiaId",
                        column: x => x.IdeiaId,
                        principalTable: "Ideia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IdeiaAnexo_IdeiaId",
                table: "IdeiaAnexo",
                column: "IdeiaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IdeiaAnexo");
        }
    }
}
