using Microsoft.EntityFrameworkCore.Migrations;

namespace LojaVirtual.Migrations
{
    public partial class profundidadeParaComprimento : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "altura",
                table: "Produtos",
                newName: "Altura");

            migrationBuilder.RenameColumn(
                name: "profundidade",
                table: "Produtos",
                newName: "Comprimento");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Produtos",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "Produtos",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Altura",
                table: "Produtos",
                newName: "altura");

            migrationBuilder.RenameColumn(
                name: "Comprimento",
                table: "Produtos",
                newName: "profundidade");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Produtos",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "Produtos",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
