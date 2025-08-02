using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ZAMETKI_FINAL.Configurations.DataBase;
using ZAMETKI_FINAL.Model;


namespace ZAMETKI_FINAL.Database
{
    public class ApplicationDbContext : DbContext
    {
        private readonly ApplicationDbContextSettings _dbContextSettings;

        public DbSet<User> Users { get; set; }
        public DbSet<Note> Notes { get; set; }
        public virtual DbSet<JwtToken> JwtTokens { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
            IOptions<ApplicationDbContextSettings> dbContextSettings) : base(options) 
        {
            _dbContextSettings = dbContextSettings.Value; 
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            // Теперь тут настраиваем работу с PostgreSQL, а заодно используем строку подключенияиз нашего конфига.
            optionsBuilder.UseNpgsql(_dbContextSettings.ConnectionString);
            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Debug);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {           
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Program).Assembly);
        }
    }
}
