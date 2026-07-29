using Flashcards.Solomonlol.Data;
using Flashcards.Solomonlol.Dto;
using Flashcards.Solomonlol.Model;

namespace Flashcards.Solomonlol.Services
{
    internal class StackService// : IService
    {
        private readonly UnitOfWork _unitOfWork;
        public StackService(UnitOfWork unit)
        {
            _unitOfWork = unit;
        }

        //public Task AddFlashcardToStackAsync(int stackId, FlashcardDto dto, CancellationToken cancellationToken = default)
        //{
            
        //}

        public async Task CreateAsync(string name, CancellationToken cancellationToken = default)
        {
            if (name != null && name.Length<=50)
            {
                Stack stack = new Stack(name);
                await _unitOfWork.Stacks.CreateAsync(stack, cancellationToken);
            }
            else throw new InvalidOperationException("Stack name is required with length <= 50");
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            await _unitOfWork.Stacks.DeleteAsync(id, cancellationToken);
        }

        //public async Task GetAsync(string name, CancellationToken cancellationToken = default)
        //{
        //    await _repository.
        //}

        //public async Task<IEnumerable<FlashcardDto>> GetFlashcardsFromStackAsync(int stackId, CancellationToken cancellationToken = default)
        //{
        //    var list =  await _repository.GetFlashcardListAsync(cancellationToken);
        //    FlashcardDto flashcardDto = new();
        //    return list;
        //}

        public Task<SessionDto> GetSessionStatisticsAsync(int stackId, DateTime? from = null, DateTime? to = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        //public Task<IEnumerable<FlashcardDto>> GetStackListAsync(CancellationToken cancellationToken = default)
        //{

        //}

        public async Task SaveAsync(CancellationToken cancellationToken = default)
        {
            await _unitOfWork.Save();
        }

        //public Task UpdateAsync(Stack item, CancellationToken cancellationToken = default)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task UpdateAsync(int id, CancellationToken cancellationToken = default)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
