using Flashcards.Solomonlol.Data;
using Flashcards.Solomonlol.Dto;
using Flashcards.Solomonlol.Interfaces;
using Flashcards.Solomonlol.Model;
using Spectre.Console;

namespace Flashcards.Solomonlol.Services
{
    internal class StackService : IService<Stack>
    {
        private readonly UnitOfWork _unitOfWork;
        public StackService()
        {
            _unitOfWork = new UnitOfWork();
        }

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

        public async Task UpdateAsync(int id, CancellationToken cancellationToken = default)
        {
            var stack = await _unitOfWork.Stacks.GetByIdAsync(id);
            if (stack != null)
            {
                stack.StackName = AnsiConsole.Prompt(new TextPrompt<string>("Type new name"));
                await _unitOfWork.Stacks.UpdateAsync(stack);
            }
            else throw new InvalidOperationException("Stack id was not found");
        }

        public async Task SaveAsync(CancellationToken cancellationToken = default)
        {
            await _unitOfWork.Save();
        }

        //public Task AddFlashcardToStackAsync(int stackId, FlashcardDto dto, CancellationToken cancellationToken = default)
        //{

        //}

        //public async Task GetAsync(string name, CancellationToken cancellationToken = default)
        //{
        //    await _unitOfWork.Stacks.GetByIdAsync(name, cancellationToken);
        //}

        //public async Task<IEnumerable<FlashcardDto>> GetFlashcardsFromStackAsync(int stackId, CancellationToken cancellationToken = default)
        //{
        //    var list =  await _repository.GetFlashcardListAsync(cancellationToken);
        //    FlashcardDto flashcardDto = new();
        //    return list;
        //}

        //public Task<SessionDto> GetSessionStatisticsAsync(int stackId, DateTime? from = null, DateTime? to = null, CancellationToken cancellationToken = default)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<IEnumerable<Stack>> GetStackListAsync(CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Stacks.GetListAsync(cancellationToken);
        }





        //public Task UpdateAsync(int id, CancellationToken cancellationToken = default)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
