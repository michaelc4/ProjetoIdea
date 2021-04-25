using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Api.Data.Migrations
{
    public partial class UsuarioMigration_ChangeNullCollumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Voluntario_Ideia_IdeiaId",
                table: "Voluntario");

            migrationBuilder.DropForeignKey(
                name: "FK_Voluntario_Problema_ProblemaId",
                table: "Voluntario");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProblemaId",
                table: "Voluntario",
                type: "char(36)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "char(36)");

            migrationBuilder.AlterColumn<Guid>(
                name: "IdeiaId",
                table: "Voluntario",
                type: "char(36)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "char(36)");

            migrationBuilder.AddForeignKey(
                name: "FK_Voluntario_Ideia_IdeiaId",
                table: "Voluntario",
                column: "IdeiaId",
                principalTable: "Ideia",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Voluntario_Problema_ProblemaId",
                table: "Voluntario",
                column: "ProblemaId",
                principalTable: "Problema",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Voluntario_Ideia_IdeiaId",
                table: "Voluntario");

            migrationBuilder.DropForeignKey(
                name: "FK_Voluntario_Problema_ProblemaId",
                table: "Voluntario");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProblemaId",
                table: "Voluntario",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "IdeiaId",
                table: "Voluntario",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Voluntario_Ideia_IdeiaId",
                table: "Voluntario",
                column: "IdeiaId",
                principalTable: "Ideia",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Voluntario_Problema_ProblemaId",
                table: "Voluntario",
                column: "ProblemaId",
                principalTable: "Problema",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
