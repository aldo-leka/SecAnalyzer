using Microsoft.EntityFrameworkCore;

namespace SecAnalyzer.Models
{
    public class SecAnalyzerContext : DbContext
    {
        public DbSet<Config> Config { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Log> Logs { get; set; }

        public SecAnalyzerContext(DbContextOptions<SecAnalyzerContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Data Source=(LocalDb)\MSSQLLocalDB;Database=SecAnalyzer;Integrated Security=True;");
            }
        }
    }
}
