using Flashcards.Solomonlol.Model;
using Microsoft.EntityFrameworkCore;

namespace Flashcards.Solomonlol
{
    internal class ApplicationContext: DbContext
    {
        private readonly string _connectionString;
        public DbSet<Stack> Stacks { get; set; } = null!;
        public DbSet<Flashcard> Flashcards { get; set; }
        public ApplicationContext(string connectionString)
        {
            _connectionString = connectionString;
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
            //modelBuilder.Entity<Flashcard>()
            //    .HasOne(f => f.Stack)
            //    .WithMany(s => s.flashcards)
            //    .HasForeignKey(f => new { f.StackID, f.StackName });
        }
    }
}
