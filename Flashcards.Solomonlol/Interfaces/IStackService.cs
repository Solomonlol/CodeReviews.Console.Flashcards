using Flashcards.Solomonlol.Data;

namespace Flashcards.Solomonlol.Interfaces
{
    internal interface IStackService
    {
        Task CreateAsync(string name, CancellationToken cancellationToken = default);
        Task UpdateAsync(string name, CancellationToken cancellationToken = default);
        Task DeleteAsync(string name, CancellationToken cancellationToken = default);
        Task SaveAsync(UnitOfWork unit, CancellationToken cancellationToken = default);
    }
}
