using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CamisasApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Camisas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Tamanho = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Classe = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Camisas", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Camisas",
                columns: new[] { "Id", "Classe", "Nome", "Tamanho", "Valor" },
                values: new object[,]
                {
                    { 1, 1, "Corinthians", "GG", 500m },
                    { 2, 2, "Palmeiras", "P", 50m },
                    { 3, 0, "Vasco", "G", 150m },
                    { 4, 1, "São Paulo", "M", 250m },
                    { 5, 2, "Santos", "GG", 70m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Camisas");
        }
    }
}
