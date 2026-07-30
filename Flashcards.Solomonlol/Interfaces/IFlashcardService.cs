using Flashcards.Solomonlol.Model.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Flashcards.Solomonlol.Interfaces
{
    internal interface IFlashcardService
    {
        //Task<IEnumerable<FlashcardDto>> GetStackListAsync(CancellationToken cancellationToken = default);
        //Task GetAsync(string name, CancellationToken cancellationToken = default);
        Task CreateAsync(int stackId, FlashcardDto dto, CancellationToken cancellationToken = default);
        Task UpdateAsync(int id, CancellationToken cancellationToken = default);
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);
        Task SaveAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<FlashcardDto>> GetFlashcardsFromStackNameAsync(string stackName, CancellationToken cancellationToken = default);
        //Task<IEnumerable<SessionDto>> GetSessionStatisticsAsync(int stackId, DateTime? from = null, DateTime? to = null, CancellationToken cancellationToken = default);
    }
}
