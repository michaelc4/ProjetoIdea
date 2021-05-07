using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Api.Data.Migrations
{
    public partial class UsuarioMigration_ChangeLike_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Like_Ideia_IdeiaId",
                table: "Like");

            migrationBuilder.DropForeignKey(
                name: "FK_Like_Problema_ProblemaId",
                table: "Like");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProblemaId",
                table: "Like",
                type: "char(36)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "char(36)");

            migrationBuilder.AlterColumn<Guid>(
                name: "IdeiaId",
                table: "Like",
                type: "char(36)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "char(36)");

            migrationBuilder.AddForeignKey(
                name: "FK_Like_Ideia_IdeiaId",
                table: "Like",
                column: "IdeiaId",
                principalTable: "Ideia",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Like_Problema_ProblemaId",
                table: "Like",
                column: "ProblemaId",
                principalTable: "Problema",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Like_Ideia_IdeiaId",
                table: "Like");

            migrationBuilder.DropForeignKey(
                name: "FK_Like_Problema_ProblemaId",
                table: "Like");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProblemaId",
                table: "Like",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "IdeiaId",
                table: "Like",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Like_Ideia_IdeiaId",
                table: "Like",
                column: "IdeiaId",
                principalTable: "Ideia",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Like_Problema_ProblemaId",
                table: "Like",
                column: "ProblemaId",
                principalTable: "Problema",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
