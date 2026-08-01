using Flashcards.Solomonlol.Model;

namespace Flashcards.Solomonlol.Interfaces
{
    internal interface IFlashcardRepository
    {
        Task<IEnumerable<Flashcard>> GetListAsync(CancellationToken cancellationToken = default);
        Task<Flashcard?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<Flashcard?> GetByQuestionAsync(int stackId, string question, CancellationToken cancellationToken = default);
        Task CreateAsync(Flashcard item, CancellationToken cancellationToken = default);
        Task UpdateAsync(Flashcard item, CancellationToken cancellationToken = default);
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    }
}
