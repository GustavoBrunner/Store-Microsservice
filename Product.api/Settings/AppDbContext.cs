using Microsoft.EntityFrameworkCore;
using ProductApi.Models;

namespace ProductApi.Settings{
    public class AppDbContext : DbContext{
        public DbSet<CategoryModel> Categories { get; private set; }

        public DbSet<ProductModel> Products { get; private set; }
        

        public AppDbContext(DbContextOptions options) : base(options){}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Categories settings

            modelBuilder.Entity<CategoryModel>()
                .HasKey(c => c.CategoryId);

            modelBuilder.Entity<CategoryModel>()
                .Property(c => c.Name)
                .HasMaxLength(100);

            modelBuilder.Entity<CategoryModel>()
                .HasMany(c => c.Products)
                .WithOne(p => p.Category);

            //Products settings
            modelBuilder.Entity<ProductModel>()
                .HasKey(p => p.ProductId);

            modelBuilder.Entity<ProductModel>()
                .Property(p => p.Description)
                .HasMaxLength(255);

            modelBuilder.Entity<ProductModel>()
                .Property(p => p.Name)
                .HasMaxLength(255);
            
            modelBuilder.Entity<ProductModel>()
                .Property(p => p.Image)
                .HasMaxLength(255);

            modelBuilder.Entity<ProductModel>()
                .Property( p => p.Price )
                //how many numbers and how many decimals
                .HasPrecision(12,3);
        }
    }
}