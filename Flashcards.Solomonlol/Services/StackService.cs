using Flashcards.Solomonlol.Data;
using Flashcards.Solomonlol.Interfaces;
using Flashcards.Solomonlol.Model;
using Spectre.Console;

namespace Flashcards.Solomonlol.Services
{
    internal class StackService : IStackService
    {
        public StackService()
        {
        }

        public async Task CreateAsync(string name, CancellationToken cancellationToken = default)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork())
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
                    ExceptionMessage(ex);
                }
                finally
                {
                    await SaveAsync(_unitOfWork, cancellationToken);
                }
            }
        }

        public async Task DeleteAsync(string name, CancellationToken cancellationToken = default)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork())
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
                    ExceptionMessage(ex);
                }
                finally
                {
                    await SaveAsync(_unitOfWork, cancellationToken);
                }
            }
        }

        public async Task UpdateAsync(string name, CancellationToken cancellationToken = default)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork())
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
                catch (Exception ex)
                {
                    ExceptionMessage(ex);
                }
                finally
                {
                    await SaveAsync(_unitOfWork, cancellationToken);
                }
            }
        }

        public async Task SaveAsync(UnitOfWork _unitOfWork, CancellationToken cancellationToken = default)
        {
            try
            {
                    await _unitOfWork.Save();
            }
            catch(Exception ex) 
            {
                ExceptionMessage(ex);
            }
        }

        public async Task<IEnumerable<Stack>> GetStackListAsync(CancellationToken cancellationToken = default)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork())
                return await _unitOfWork.Stacks.GetListAsync(cancellationToken);
        }

        public async Task<Stack> GetStackByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork())
            {
                var stack = await _unitOfWork.Stacks.GetByNameAsync(name);
                if(stack!=null)
                {  return stack; }
                else throw new Exception($"Stack with name {name} doesn't exists.");
            }
        }

        private void ExceptionMessage(Exception ex)
        {
            
            AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
            AnsiConsole.MarkupLine("Press any key to continue...");
                Console.ReadKey();
            AnsiConsole.Clear();
        }
    }
}
