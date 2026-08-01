using Flashcards.Solomonlol.Interfaces;
using Flashcards.Solomonlol.Model;
using Microsoft.EntityFrameworkCore;
using Spectre.Console;

namespace Flashcards.Solomonlol.Data
{
    internal class FlashcardRepository : IFlashcardRepository
    {
        private ApplicationContext _db;
        public FlashcardRepository(ApplicationContext context)
        {
            _db = context;
        }
        public async Task CreateAsync(Flashcard item, CancellationToken cancellationToken = default)
        {
            await _db.Flashcards.AddAsync(item, cancellationToken);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var item = await _db.Flashcards.FindAsync(id, cancellationToken);
            if (item != null)
            {
                _db.Flashcards.Remove(item);
            }
            else AnsiConsole.MarkupLine($"[red]Flashcard with id={id} was not found.[/]");
        }

        public async Task UpdateAsync(Flashcard item, CancellationToken cancellationToken = default)
        {
            _db.Entry(item).State = EntityState.Modified;
        }
        public async Task<Flashcard?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _db.Flashcards.FindAsync(id, cancellationToken);
        }

        public async Task<IEnumerable<Flashcard>> GetListAsync(CancellationToken cancellationToken = default)
        {
            return await _db.Flashcards.ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Flashcard>> GetListByStackIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _db.Flashcards.Where(s=>s.StackID == id).ToListAsync(cancellationToken);
        }

        public async Task<Flashcard?> GetByQuestionAsync(int stackId, string question, CancellationToken cancellationToken = default)
        {
            return await _db.Flashcards.FirstOrDefaultAsync(f => f.Question == question && f.StackID==stackId);
        }

        public async Task<Flashcard?> GetByFlashAndStackIdAsync(int stackId, int flashcardId, CancellationToken cancellationToken = default)
        {
            return await _db.Flashcards.FirstOrDefaultAsync(f => f.StackID == stackId && f.Id == flashcardId);
        }
    }
}
