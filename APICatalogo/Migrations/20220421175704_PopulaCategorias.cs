using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APICatalogo.Migrations
{
    public partial class PopulaCategorias : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder) //abaixo temos a população dos dados
        {
            migrationBuilder.Sql("Insert into Categorias(Nome,ImageUrl) Values('Bebidas','bebidas.jpg')");
            migrationBuilder.Sql("Insert into Categorias(Nome,ImageUrl) Values('Lanches','lanches.jpg')");
            migrationBuilder.Sql("Insert into Categorias(Nome,ImageUrl) Values('Sobremesas','sobremesas.jpg')");
        }

        protected override void Down(MigrationBuilder migrationBuilder) //abaixo temos um comando caso queiramos deletar os dados
        {
            migrationBuilder.Sql("Delete from Categorias");
        }
    }
}
