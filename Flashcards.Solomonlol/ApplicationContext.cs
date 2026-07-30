using Flashcards.Solomonlol.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Flashcards.Solomonlol
{
    internal class ApplicationContext: DbContext
    {
        private readonly string _connectionString;
        public DbSet<Stack> Stacks { get; set; }
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
                .HasIndex(s => s.Name)
                .IsUnique();

            modelBuilder.Entity<Flashcard>()
                .HasIndex(s => s.Question)
                .IsUnique();

            modelBuilder.Entity<Flashcard>()
                .HasOne(f => f.Stack)
                .WithMany(s => s.Flashcards)
                .HasForeignKey(f => f.StackID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SessionHistory>()
                .HasOne(sh => sh.Stack)
                .WithMany(s => s.Sessions)
                .HasForeignKey(sh => sh.StackID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
