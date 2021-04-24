using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Api.Data.Migrations
{
    public partial class UsuarioMigration_AddLike_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Like",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    IpUsr = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    IdeiaId = table.Column<Guid>(type: "char(36)", nullable: false),
                    ProblemaId = table.Column<Guid>(type: "char(36)", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Like", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Like_Ideia_IdeiaId",
                        column: x => x.IdeiaId,
                        principalTable: "Ideia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Like_Problema_ProblemaId",
                        column: x => x.ProblemaId,
                        principalTable: "Problema",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Like_IdeiaId",
                table: "Like",
                column: "IdeiaId");

            migrationBuilder.CreateIndex(
                name: "IX_Like_ProblemaId",
                table: "Like",
                column: "ProblemaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Like");
        }
    }
}
