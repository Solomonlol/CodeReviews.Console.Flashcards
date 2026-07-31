using Flashcards.Solomonlol.Data;

namespace Flashcards.Solomonlol.Interfaces
{
    internal interface IFlashcardService
    {
        Task CreateAsync(string stackName, string question, string answer, CancellationToken cancellationToken = default);
        Task UpdateAsync(string stackName, int flashcardId, CancellationToken cancellationToken = default);
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);
        Task SaveAsync(UnitOfWork unit, CancellationToken cancellationToken = default);
    }
}
