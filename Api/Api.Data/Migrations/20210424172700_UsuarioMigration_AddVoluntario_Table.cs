using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Api.Data.Migrations
{
    public partial class UsuarioMigration_AddVoluntario_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Voluntario",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "char(36)", nullable: false),
                    IdeiaId = table.Column<Guid>(type: "char(36)", nullable: false),
                    ProblemaId = table.Column<Guid>(type: "char(36)", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Voluntario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Voluntario_Ideia_IdeiaId",
                        column: x => x.IdeiaId,
                        principalTable: "Ideia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Voluntario_Problema_ProblemaId",
                        column: x => x.ProblemaId,
                        principalTable: "Problema",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Voluntario_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Voluntario_IdeiaId",
                table: "Voluntario",
                column: "IdeiaId");

            migrationBuilder.CreateIndex(
                name: "IX_Voluntario_ProblemaId",
                table: "Voluntario",
                column: "ProblemaId");

            migrationBuilder.CreateIndex(
                name: "IX_Voluntario_UsuarioId",
                table: "Voluntario",
                column: "UsuarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Voluntario");
        }
    }
}
