using Flashcards.Solomonlol.Data;
using Flashcards.Solomonlol.Interfaces;
using Flashcards.Solomonlol.Model;
using Flashcards.Solomonlol.Model.Dto;
using Spectre.Console;

namespace Flashcards.Solomonlol.Services
{
    internal class SessionService : ISessionService
    {
        public async Task CreateAsync(DateTime dateTime, int score, string stackName, CancellationToken cancellationToken = default)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork())
            {
                try
                {
                    var checkStack = await _unitOfWork.Stacks.GetByNameAsync(stackName);
                    if (checkStack != null)
                    {
                        await _unitOfWork.Sessions.CreateAsync(new SessionHistory(dateTime, score, checkStack.Id));
                    }
                    else throw new Exception($"Stack with name {stackName} doesn't exists.");
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

        public async Task<IEnumerable<SessionDto>> GetListAsync(CancellationToken cancellationToken = default)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork())
            {
                var list = await _unitOfWork.Sessions.GetListAsync(cancellationToken);
                if (list != null)
                {
                    var listDto = new List<SessionDto>();
                    foreach (var item in list)
                    {
                        listDto.Add(new SessionDto(item));
                    }
                    return listDto;
                }
                else throw new Exception("Session history doesn't exists.");
            }
        }

        public async Task<IEnumerable<SessionDto>> GetListByStackNameAsync(string stackName, CancellationToken cancellationToken = default)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork())
            {
                var checkStack = await _unitOfWork.Stacks.GetByNameAsync(stackName);
                if (checkStack != null)
                {
                    var list = await _unitOfWork.Sessions.GetListByStackAsync(checkStack.Id);
                    if (list.Any())
                    {
                        var listDto = new List<SessionDto>();
                        foreach (var item in list)
                        {
                            listDto.Add(new SessionDto(item));
                        }
                        return listDto;
                    }
                    else throw new Exception("This stack has no session history.");
                }
                else throw new Exception($"Stack with name {stackName} doesn't exists.");
            }
        }

        public async Task SaveAsync(UnitOfWork _unitOfWork, CancellationToken cancellationToken = default)
        {
            try
            {
                await _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                ExceptionMessage(ex);
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
