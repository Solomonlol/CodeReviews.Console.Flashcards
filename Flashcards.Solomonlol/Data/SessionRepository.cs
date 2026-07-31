using Flashcards.Solomonlol.Interfaces;
using Flashcards.Solomonlol.Model;
using Microsoft.EntityFrameworkCore;

namespace Flashcards.Solomonlol.Data
{
    internal class SessionRepository : ISessionRepository
    {
        private ApplicationContext _db;
        public SessionRepository(ApplicationContext context)
        {
            _db = context;
        }
        public async Task CreateAsync(SessionHistory item, CancellationToken cancellationToken = default)
        {
            await _db.Sessions.AddAsync(item, cancellationToken);
        }

        public async Task<IEnumerable<SessionHistory>> GetListAsync(CancellationToken cancellationToken = default)
        {
            return await _db.Sessions.ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<SessionHistory>> GetListByStackAsync(int stackId, CancellationToken cancellationToken = default)
        {
            return (await _db.Sessions.ToListAsync(cancellationToken)).Where(s => s.StackID == stackId);
        }
    }
}
