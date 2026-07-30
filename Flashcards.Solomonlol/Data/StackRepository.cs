using Flashcards.Solomonlol.Interfaces;
using Flashcards.Solomonlol.Model;
using Microsoft.EntityFrameworkCore;

namespace Flashcards.Solomonlol.Data
{
    internal class StackRepository : IStackRepository
    {
        private ApplicationContext _db;
        public StackRepository(ApplicationContext context)
        {
            _db = context;
        }
        public async Task CreateAsync(Stack stack, CancellationToken cancellationToken = default)
        {
            await _db.Stacks.AddAsync(stack, cancellationToken);
        }

        public async Task DeleteAsync(string name, CancellationToken cancellationToken = default)
        {
            Stack? stack = await _db.Stacks.FirstOrDefaultAsync(stack => stack.Name == name);
            if(stack!=null)
            {
                _db.Stacks.Remove(stack);
            }
        }

        public Task UpdateAsync(Stack stack, CancellationToken cancellationToken = default)
        {
            _db.Entry(stack).State=EntityState.Modified;
            return Task.CompletedTask;
        }

        public async Task<Stack?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _db.Stacks.FindAsync(id, cancellationToken);
        }

        public async Task<IEnumerable<Stack>> GetListAsync(CancellationToken cancellationToken = default)
        {
            return await _db.Stacks.ToListAsync(cancellationToken);
        }

        public async Task<Stack?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await _db.Stacks.FirstOrDefaultAsync(stack=>stack.Name==name);
        }
    }
}
