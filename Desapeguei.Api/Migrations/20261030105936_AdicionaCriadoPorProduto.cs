using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Desapeguei.Api.Migrations
{
    /// <inheritdoc />
    public partial class AdicionaCriadoPorProduto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CriadoPor",
                table: "Produtos",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CriadoPor",
                table: "Produtos");
        }
    }
}
