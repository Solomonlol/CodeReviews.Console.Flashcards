namespace Flashcards.Solomonlol.Interfaces
{
    internal interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetListAsync(CancellationToken cancellationToken = default);
        Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task CreateAsync(T item, CancellationToken cancellationToken = default);
        Task UpdateAsync(T item, CancellationToken cancellationToken = default);
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    }
}
