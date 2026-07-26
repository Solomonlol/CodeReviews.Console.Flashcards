using Flashcards.Solomonlol.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Flashcards.Solomonlol
{
    internal class ApplicationContext: DbContext
    {
        private readonly string _connectionString;
        public DbSet<Stack> Stacks { get; set; } = null!;
        public DbSet<Flashcard> Flashcards { get; set; }
        public DbSet<SessionHistory> Sessions { get; set; }
        public ApplicationContext()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Stack>()
                .HasIndex(s => s.StackName)
                .IsUnique();
            modelBuilder.Entity<Stack>()
                .HasMany(s => s.Flashcards)
                .WithOne(f => f.Stack)
                .HasForeignKey(f => f.StackID);
            modelBuilder.Entity<Stack>()
                .HasMany(s => s.Sessions)
                .WithOne(f => f.Stack)
                .HasForeignKey(f => f.StackID);
        }
    }
}
