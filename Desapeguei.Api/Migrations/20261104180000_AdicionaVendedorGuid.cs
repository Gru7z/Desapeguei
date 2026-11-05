using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Desapeguei.Api.Migrations
{
    /// <inheritdoc />
    public partial class AdicionaVendedorGuid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "VendedorGuid",
                table: "Produtos",
                type: "uniqueidentifier",
                nullable: true);

            // Preserva os dados: tudo que estava em VendedorId e for um Guid
            // válido é copiado para a nova coluna VendedorGuid.
            migrationBuilder.Sql(
                @"UPDATE [Produtos]
                  SET [VendedorGuid] = TRY_CAST([VendedorId] AS uniqueidentifier)
                  WHERE [VendedorId] IS NOT NULL;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VendedorGuid",
                table: "Produtos");
        }
    }
}
