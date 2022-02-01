using ETicaretWebApi.Entitites;
using Microsoft.EntityFrameworkCore;

namespace ETicaretWebApi.DbOperations
{
    public class ETicaretDbContext: DbContext
    {
        private readonly IConfiguration _configuration;
        public ETicaretDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<MainPageCategory> MainPageCategories { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<CartCartItem> CartCartItems { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration["DbConnectionString"]);
        }
    }
}
