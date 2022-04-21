using APICatalogo.Models;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {}//classe de contexto

        public DbSet<Categoria>? Categorias { get; set; } // Categorias essa declaração deixa eu acessar no controller
        public DbSet<Produto>? Produtos { get; set; } // Produtos essa declaração deixa eu acessar no controller

    }
}
