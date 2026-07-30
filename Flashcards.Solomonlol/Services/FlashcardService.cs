using Flashcards.Solomonlol.Data;
using Flashcards.Solomonlol.Interfaces;
using Flashcards.Solomonlol.Model;
using Flashcards.Solomonlol.Model.Dto;
using Spectre.Console;

namespace Flashcards.Solomonlol.Services
{
    internal class FlashcardService// : IFlashcardService
    {
        private readonly UnitOfWork _unitOfWork;
        public FlashcardService()
        {
            _unitOfWork = new UnitOfWork();
        }
        public async Task CreateAsync(int stackId, FlashcardDto fDto, CancellationToken cancellationToken = default)
        {
            try
            {
                var checkFlashcard = await _unitOfWork.Flashcards.GetByNameAsync(fDto.Question);
                if (checkFlashcard == null)
                {
                    if (fDto.Question != null && fDto.Question.Length <= 100)
                    {
                        var flashcard = new Flashcard()
                        {
                            Question = fDto.Question,
                            Answer = fDto.Answer,
                            StackID = stackId,
                        };
                        await _unitOfWork.Flashcards.CreateAsync(flashcard, cancellationToken);
                    }
                    else throw new Exception("Flashcard questions and answers is required with length <= 100");
                }
                else throw new Exception($"Stack with name [red]{fDto.Question}[/] already exists");
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

        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                var flashcard = await _unitOfWork.Flashcards.GetByIdAsync(id);
                if (flashcard != null)
                {
                    await _unitOfWork.Flashcards.DeleteAsync(id, cancellationToken);
                }
                else throw new Exception("Flashcard id was not found");
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

        public async Task SaveAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                await _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                ExeptionMessage(ex);
            }
        }

        public async Task UpdateAsync(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                var flashcard = await _unitOfWork.Flashcards.GetByIdAsync(id);
                if (flashcard != null)
                {
                    var question = AnsiConsole.Prompt(new TextPrompt<string>("Type new question"));
                    var checkFlashcard = await _unitOfWork.Flashcards.GetByNameAsync(question);
                    if (checkFlashcard == null)
                    {
                        var answer = AnsiConsole.Prompt(new TextPrompt<string>("Type new answer"));
                        flashcard.Question = question;
                        flashcard.Answer = answer;
                        await _unitOfWork.Flashcards.UpdateAsync(flashcard);
                    }
                    else throw new Exception($"Flashcard already exists");
                }
                else throw new Exception("Stack id was not found");
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

        public async Task<IEnumerable<Flashcard>> GetListByStackIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Flashcards.GetListByStackIdAsync(id);
        }

        private void ExeptionMessage(Exception ex)
        {

            AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
            AnsiConsole.MarkupLine("Press any key to continue...");
            Console.ReadKey();
            AnsiConsole.Clear();
        }

        //public async Task<IEnumerable<FlashcardDto>> GetFlashcardsFromStackNameAsync(string stackName, CancellationToken cancellationToken = default)
        //{
        //    var flashcard = await _unitOfWork.Stacks.
        //    FlashcardDto dto = new FlashcardDto(flashcard);
        //    re
        //}
    }
}
