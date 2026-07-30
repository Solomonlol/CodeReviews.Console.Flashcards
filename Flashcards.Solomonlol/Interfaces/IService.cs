namespace Flashcards.Solomonlol.Interfaces
{
    internal interface IService<T> where T : class
    {
        //Task<IEnumerable<FlashcardDto>> GetStackListAsync(CancellationToken cancellationToken = default);
        //Task GetByNameAsync(string name, CancellationToken cancellationToken = default);
        Task CreateAsync(string name, CancellationToken cancellationToken = default);
        Task UpdateAsync(string name, CancellationToken cancellationToken = default);
        Task DeleteAsync(string name, CancellationToken cancellationToken = default);
        Task SaveAsync(CancellationToken cancellationToken = default);
        //Task AddFlashcardToStackAsync(int stackId, FlashcardDto dto, CancellationToken cancellationToken = default);
        //Task<IEnumerable<FlashcardDto>> GetFlashcardsFromStackAsync(int stackId, CancellationToken cancellationToken = default);
        //Task<IEnumerable<SessionDto>> GetSessionStatisticsAsync(int stackId, DateTime? from = null, DateTime? to = null, CancellationToken cancellationToken = default);

    }
}
