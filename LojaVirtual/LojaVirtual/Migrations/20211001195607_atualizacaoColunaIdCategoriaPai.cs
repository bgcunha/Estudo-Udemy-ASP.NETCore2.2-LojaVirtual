using Microsoft.EntityFrameworkCore.Migrations;

namespace LojaVirtual.Migrations
{
    public partial class atualizacaoColunaIdCategoriaPai : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categorias_Categorias_idCategoriaPai",
                table: "Categorias");

            migrationBuilder.RenameColumn(
                name: "idCategoriaPai",
                table: "Categorias",
                newName: "IdCategoriaPai");

            migrationBuilder.RenameIndex(
                name: "IX_Categorias_idCategoriaPai",
                table: "Categorias",
                newName: "IX_Categorias_IdCategoriaPai");

            migrationBuilder.AlterColumn<string>(
                name: "Slug",
                table: "Categorias",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Categorias_Categorias_IdCategoriaPai",
                table: "Categorias",
                column: "IdCategoriaPai",
                principalTable: "Categorias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categorias_Categorias_IdCategoriaPai",
                table: "Categorias");

            migrationBuilder.RenameColumn(
                name: "IdCategoriaPai",
                table: "Categorias",
                newName: "idCategoriaPai");

            migrationBuilder.RenameIndex(
                name: "IX_Categorias_IdCategoriaPai",
                table: "Categorias",
                newName: "IX_Categorias_idCategoriaPai");

            migrationBuilder.AlterColumn<string>(
                name: "Slug",
                table: "Categorias",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddForeignKey(
                name: "FK_Categorias_Categorias_idCategoriaPai",
                table: "Categorias",
                column: "idCategoriaPai",
                principalTable: "Categorias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
