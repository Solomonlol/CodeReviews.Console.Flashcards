using Flashcards.Solomonlol.Model;

namespace Flashcards.Solomonlol.Interfaces
{
    internal interface ISessionRepository
    {
        Task<IEnumerable<SessionHistory>> GetListAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<SessionHistory>> GetListByStackAsync(int stackId, CancellationToken cancellationToken = default);
        Task CreateAsync(SessionHistory item, CancellationToken cancellationToken = default);
    }
}
