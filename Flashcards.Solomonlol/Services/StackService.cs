using Flashcards.Solomonlol.Data;
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
            try
            {
                var checkStack = await _unitOfWork.Stacks.GetByNameAsync(name);
                if (checkStack == null)
                {
                    if (name != null && name.Length <= 50)
                    {
                        var stack = new Stack(name);
                        await _unitOfWork.Stacks.CreateAsync(stack, cancellationToken);
                    }
                    else throw new Exception("Stack name is required with length <= 50");
                }
                else throw new Exception($"Stack with name [red]{name}[/] already exists");
            }
            catch (Exception ex)
            {
                ExeptionMessage(ex);
            }
            finally
            {
                await SaveAsync(cancellationToken);
            }
        }

        public async Task DeleteAsync(string name, CancellationToken cancellationToken = default)
        {
            try
            {
                var stack = await _unitOfWork.Stacks.GetByNameAsync(name);
                if (stack != null)
                {
                    await _unitOfWork.Stacks.DeleteAsync(name, cancellationToken);
                }
                else throw new Exception("Stack was not found");
            }
            catch (Exception ex)
            {
                ExeptionMessage(ex);
            }
            finally 
            { 
                await SaveAsync(cancellationToken);
            }
        }

        public async Task UpdateAsync(string name, CancellationToken cancellationToken = default)
        {
            try
            {
                var stack = await _unitOfWork.Stacks.GetByNameAsync(name);
                if (stack != null)
                {
                    var checkName = AnsiConsole.Prompt(new TextPrompt<string>("Type new name"));
                    var checkStack = await _unitOfWork.Stacks.GetByNameAsync(checkName);
                    if (checkStack == null)
                    {
                        stack.Name = checkName;
                        await _unitOfWork.Stacks.UpdateAsync(stack);
                    }
                    else throw new Exception($"Stack with name {stack.Name} already exists");
                }
                else throw new Exception("Stack id was not found");
            }
            catch(Exception ex)
            {
                ExeptionMessage(ex);
            }
            finally
            {
                await SaveAsync(cancellationToken);
            }
        }

        public async Task SaveAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                await _unitOfWork.Save();
            }
            catch(Exception ex) 
            {
                ExeptionMessage(ex);
            }
        }

        public async Task<IEnumerable<Stack>> GetStackListAsync(CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Stacks.GetListAsync(cancellationToken);
        }

        private void ExeptionMessage(Exception ex)
        {
            
            AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
            AnsiConsole.MarkupLine("Press any key to continue...");
                Console.ReadKey();
            AnsiConsole.Clear();
        }
        //public async Task GetByNameAsync(string name, CancellationToken cancellationToken = default)
        //{
             
        //}

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








        //public Task UpdateAsync(int id, CancellationToken cancellationToken = default)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
