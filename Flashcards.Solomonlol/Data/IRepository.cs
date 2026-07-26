using System;
using System.Collections.Generic;
using System.Text;

namespace Flashcards.Solomonlol.Data
{
    internal interface IRepository<T> : IDisposable where T : class
    {
        IEnumerable<T> GetStackList();
        Task GetStackAsync(int id);
        Task CreateAsync(T item);
        Task UpdateAsync(T item);
        Task DeleteAsync(int id);
        Task SaveAsync();
    }
}
