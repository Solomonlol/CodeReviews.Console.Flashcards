using Flashcards.Solomonlol.Model;

namespace Flashcards.Solomonlol.Interfaces
{
    internal interface IStackRepository
    {
        Task<IEnumerable<Stack>> GetListAsync(CancellationToken cancellationToken = default);
        Task<Stack?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<Stack?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
        Task CreateAsync(Stack item, CancellationToken cancellationToken = default);
        Task UpdateAsync(Stack item, CancellationToken cancellationToken = default);
        Task DeleteAsync(string name, CancellationToken cancellationToken = default);
    }
}
