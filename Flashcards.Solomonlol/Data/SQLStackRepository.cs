using Flashcards.Solomonlol.Model;
using Microsoft.EntityFrameworkCore;

namespace Flashcards.Solomonlol.Data
{
    internal class SQLStackRepository : IRepository<Stack>
    {
        private ApplicationContext db;
        public SQLStackRepository()
        {
            this.db = new ApplicationContext();
        }
        public async Task CreateAsync(Stack stack)
        {
            await db.Stacks.AddAsync(stack);
        }

        public async Task DeleteAsync(int id)
        {
            Stack stack = await db.Stacks.FindAsync(id);
            if(stack!=null)
            {
                db.Stacks.Remove(stack);
            }
        }
        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Stack stack)
        {
            await Task.Delay(0);
            db.Entry(stack).State=EntityState.Modified;
        }


        public async Task GetStackAsync(int id)
        {
           await db.Stacks.FindAsync(id);
        }

        public IEnumerable<Stack> GetStackList()
        {
            return db.Stacks;
        }

        public void Dispose()
        {
            db.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
