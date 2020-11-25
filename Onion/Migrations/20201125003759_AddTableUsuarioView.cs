using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Onion.Migrations
{
    public partial class AddTableUsuarioView : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Prioridade",
                table: "Tarefas",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "UsuarioViewId",
                table: "Projetos",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CriadoEm = table.Column<DateTime>(nullable: false),
                    Nome = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Senha = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Projetos_UsuarioViewId",
                table: "Projetos",
                column: "UsuarioViewId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projetos_Usuarios_UsuarioViewId",
                table: "Projetos",
                column: "UsuarioViewId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projetos_Usuarios_UsuarioViewId",
                table: "Projetos");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Projetos_UsuarioViewId",
                table: "Projetos");

            migrationBuilder.DropColumn(
                name: "UsuarioViewId",
                table: "Projetos");

            migrationBuilder.AlterColumn<int>(
                name: "Prioridade",
                table: "Tarefas",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
