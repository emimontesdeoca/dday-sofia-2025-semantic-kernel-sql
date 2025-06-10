using Microsoft.EntityFrameworkCore;
using SemanticKernel.Models;

namespace SemanticKernel
{
    public class Context : DbContext
    {
        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Payment> Payments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=PizzaShopDb;Trusted_Connection=True;TrustServerCertificate=True;");
        }
    }
}
