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
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration["DbConnectionString"]);
        }
    }
}
