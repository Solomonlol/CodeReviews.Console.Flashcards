using Flashcards.Solomonlol.Data;
using Flashcards.Solomonlol.Interfaces;
using Flashcards.Solomonlol.Model;
using Flashcards.Solomonlol.Model.Dto;
using Spectre.Console;

namespace Flashcards.Solomonlol.Services
{
    internal class FlashcardService : IFlashcardService
    {
        public FlashcardService()
        {
        }
        public async Task CreateAsync(string stackName, string question, string answer, CancellationToken cancellationToken = default)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork())
            {
                try
                {

                    var checkFlashcard = await _unitOfWork.Stacks.GetByNameAsync(stackName);
                    if (checkFlashcard != null)
                    {
                        if (question != null && question.Length <= 100)
                        {
                            var flashcard = new Flashcard()
                            {
                                Question = question,
                                Answer = answer,
                                StackID = checkFlashcard.Id,
                            };
                            await _unitOfWork.Flashcards.CreateAsync(flashcard, cancellationToken);
                        }
                        else throw new Exception("Flashcard questions and answers is required with length <= 100.");
                    }
                    else throw new Exception($"Stack with name [red]{stackName}[/] doesn't exists.");
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

        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork())
            {
                try
                {
                    var flashcard = await _unitOfWork.Flashcards.GetByIdAsync(id);
                    if (flashcard != null)
                    {
                        await _unitOfWork.Flashcards.DeleteAsync(id, cancellationToken);
                    }
                    else throw new Exception("Flashcard id was not found.");
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

        public async Task UpdateAsync(string stackName, int flashcardId , CancellationToken cancellationToken = default)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork())
            {
                try
                {
                    var stackCheck = await _unitOfWork.Stacks.GetByNameAsync(stackName);
                    if (stackCheck != null)
                    {
                        var flashcard = await _unitOfWork.Flashcards.GetByIdAsync(flashcardId);
                        if (flashcard != null)
                        {
                            var question = AnsiConsole.Prompt(new TextPrompt<string>("Type new question:"));
                            var checkQuestion = await _unitOfWork.Flashcards.GetByQuestionAsync(stackCheck.Id, question);
                            if (checkQuestion == null)
                            {
                                var answer = AnsiConsole.Prompt(new TextPrompt<string>("Type new answer:"));

                                flashcard.Question = question;
                                flashcard.Answer = answer;

                                await _unitOfWork.Flashcards.UpdateAsync(flashcard);
                            }
                            else throw new Exception("Flashcard in this stack with that exact question is exists.");
                        }
                        else throw new Exception("Flashcard was not found.");
                        
                    }
                    else throw new Exception($"Stack with name {stackName} was not found.");

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

        public async Task<IEnumerable<FlashcardDto>> GetListByStackNameAsync(string stackName, CancellationToken cancellationToken = default)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork())
            {
                var check = await _unitOfWork.Stacks.GetByNameAsync(stackName, cancellationToken);
                if (check != null)
                {
                    var list = await _unitOfWork.Flashcards.GetListByStackIdAsync(check.Id);
                    var dtoList = new List<FlashcardDto>();
                    foreach (var item in list)
                    {
                        dtoList.Add(new FlashcardDto(item));
                    }
                    return dtoList;
                }
                else throw new Exception($"Stack with name {stackName} doesn't exists.");
            }
        }

        public async Task<FlashcardDto> GetByStackNameAsync(string stackName, int flashId, CancellationToken cancellationToken = default)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork())
            {
                var check = await _unitOfWork.Stacks.GetByNameAsync(stackName, cancellationToken);
                if(check!=null)
                {
                    var flashcard = await _unitOfWork.Flashcards.GetByFlashAndStackIdAsync(check.Id, flashId);
                    
                    if (flashcard != null)
                    {
                        var flashcardDto = new FlashcardDto(flashcard);
                        return flashcardDto;
                    }
                    else throw new Exception("Flashcard doesn't exists.");
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
