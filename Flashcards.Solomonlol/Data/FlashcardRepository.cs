using Flashcards.Solomonlol.Interfaces;
using Flashcards.Solomonlol.Model;
using Microsoft.EntityFrameworkCore;
using Spectre.Console;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Flashcards.Solomonlol.Data
{
    internal class FlashcardRepository : IRepository<Flashcard>
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

    }
}
