using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatalogoApi.Migrations
{
    /// <inheritdoc />
    public partial class PopulaCategorias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("insert into Categorias (Nome, ImageUrl) Values ('Bebidas', 'bebidas.jpg')");
            mb.Sql("insert into Categorias (Nome, ImageUrl) Values ('Laches', 'lanches.jpg')");
            mb.Sql("insert into Categorias (Nome, ImageUrl) Values ('Sobremesas', 'sobremesas.jpg')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("delete from Categorias");
        }
    }
}
