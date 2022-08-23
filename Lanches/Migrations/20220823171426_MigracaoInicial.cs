using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lanches.Migrations
{
    public partial class MigracaoInicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    CATEGORIA_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CATEGORIA_NOME = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DESCRICAO = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.CATEGORIA_ID);
                });

            migrationBuilder.CreateTable(
                name: "Lanches",
                columns: table => new
                {
                    LANCHE_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NOME = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    DESCRICAO = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DESCRICAO_DETALHADA = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PRECO = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    IMAGEM = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    THUMBNAIL = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    PREFERIDO = table.Column<bool>(type: "bit", nullable: false),
                    ESTOQUE = table.Column<bool>(type: "bit", nullable: false),
                    CATEGORIA_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lanches", x => x.LANCHE_ID);
                    table.ForeignKey(
                        name: "FK_Lanches_Categorias_CATEGORIA_ID",
                        column: x => x.CATEGORIA_ID,
                        principalTable: "Categorias",
                        principalColumn: "CATEGORIA_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lanches_CATEGORIA_ID",
                table: "Lanches",
                column: "CATEGORIA_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Lanches");

            migrationBuilder.DropTable(
                name: "Categorias");
        }
    }
}
