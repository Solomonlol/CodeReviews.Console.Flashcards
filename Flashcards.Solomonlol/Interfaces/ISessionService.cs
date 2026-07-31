using Flashcards.Solomonlol.Data;
using Flashcards.Solomonlol.Model.Dto;

namespace Flashcards.Solomonlol.Interfaces
{
    internal interface ISessionService
    {
        Task CreateAsync(DateTime dateTime, int score, string stackName, CancellationToken cancellationToken = default);
        Task<IEnumerable<SessionDto>> GetListAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<SessionDto>> GetListByStackNameAsync(string stackName, CancellationToken cancellationToken = default);
        Task SaveAsync(UnitOfWork unit, CancellationToken cancellationToken = default);
    }
}
