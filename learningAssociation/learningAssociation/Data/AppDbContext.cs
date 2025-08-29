using DotnetOneToMany.Models;
using learningAssociation.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace DotnetOneToMany.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // DbSets (Tables)
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }  

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // One-to-Many relationship setup
            modelBuilder.Entity<Product>() //we are configuring the dependent table (Product).
                .HasOne(p => p.Category)     // Product has one Category
                .WithMany(c => c.Products)   // Category has many Products
                .HasForeignKey(p => p.CategoryId);
        }
    }
}
