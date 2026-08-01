using Flashcards.Solomonlol.Model;
using Flashcards.Solomonlol.Model.Dto;
using Flashcards.Solomonlol.Services;
using Spectre.Console;

namespace Flashcards.Solomonlol.Controllers
{
    internal class MainMenu
    {
        private readonly StackService stackService = new();
        private readonly FlashcardService flashcardService = new();
        private readonly SessionService sessionService = new();
        private readonly Dictionary<string, Func<Task>> _mainMenu;
        private readonly Dictionary<string, Func<Task>> _stackMenu;
        private readonly Dictionary<string, Func<Task>> _flashcardsMenu;

        public MainMenu()
        {
            _mainMenu = new()
            {
                { "Exit", () => Exit() },
                { "View study sessions", () => ViewStudySessionsData() },
                { "Study", ()=> Study() },
                { "Manage stacks", ()=> ViewSubMenu(_stackMenu, "Stack menu") },
                { "Manage flashcards", ()=>ViewSubMenu(_flashcardsMenu, "Flashcards menu") }
            };
            _stackMenu = new()
            {
                { "Back", () => Task.CompletedTask },
                { "Create new stack", () => CreateStack() },
                { "Update stack", () => UpdateStack() },
                { "Delete stack", () => DeleteStack() }
            };
            _flashcardsMenu = new()
            {
                { "Back", () => Task.CompletedTask },
                { "View flashcards", () => ViewAllFlashcardsByStack() },
                { "Create flashcard", () => CreateFlashcard() },
                { "Update flashcard", () => UpdateFlashcard() },
                { "Delete flashcard", () => DeleteFlashcard() }
            };
        }
        public async Task Menu()
        {
            while (true)
            {
                await ViewAllStack();
                await ViewSubMenu(_mainMenu, "Main menu");
            }
        }

        private async Task ViewSubMenu(Dictionary<string, Func<Task>> menu, string title)
        {
            var choice = AnsiConsole.Prompt(new SelectionPrompt<string>()
                    .Title($"[green]{title}[/]")
                    .AddChoices(menu.Keys));

            await menu[choice]();
        }

        private async Task CreateStack()
        {
            try
            {
                string name = AnsiConsole.Prompt(
                            new TextPrompt<string>("Enter stack [green]name[/]:"));
                await stackService.CreateAsync(name);
            }
            catch (Exception ex)
            {
                Message(ex.Message);
            }
        }

        private async Task CreateFlashcard()
        {
            try
            {
                var list = await stackService.GetStackListAsync();
                if (list.Any())
                {
                    var stackName = AnsiConsole.Prompt(new SelectionPrompt<string>()
                                                    .Title("Enter the [green]name of the stack[/] to which the flashcard belongs.")
                                                    .AddChoices(list.Select(s => s.Name)));

                    string question = AnsiConsole.Prompt(
                                new TextPrompt<string>("Enter stack [green]Question[/]:"));
                    string answer = AnsiConsole.Prompt(
                                new TextPrompt<string>("Enter stack [green]Answer[/]:"));

                    await flashcardService.CreateAsync(stackName, question, answer);
                }
                else Message("No stacks was found.");
            }
            catch (Exception ex)
            {
                Message(ex.Message);
            }
        }


        private async Task UpdateStack()
        {
            try
            {
                var list = await stackService.GetStackListAsync();
                if (list.Any())
                {
                    var name = AnsiConsole.Prompt(new SelectionPrompt<string>()
                                                    .Title("Enter stack name to [red]update.[/]")
                                                    .AddChoices(list.Select(s => s.Name)));
                    if (AnsiConsole.Confirm("Are you sure?"))
                    {
                        await stackService.UpdateAsync(name);
                    }
                }
                else Message("No stacks was found.");
            }
            catch (Exception ex)
            {
                Message(ex.Message);
            }
        }

        private async Task UpdateFlashcard()
        {
            try
            {
                await ViewAllStack();
                var stackList = await stackService.GetStackListAsync();
                if (stackList.Any())
                {
                    var stackName = AnsiConsole.Prompt(new SelectionPrompt<string>()
                                                    .Title("Enter stack name to [red]update.[/]")
                                                    .AddChoices(stackList.Select(s => s.Name)));

                    var flashcardList = await flashcardService.GetListByStackNameAsync(stackName);

                    await ViewAllFlashcardsByStack(stackName, flashcardList);

                    var flashcardQuestion = AnsiConsole.Prompt(new SelectionPrompt<string>()
                                                    .Title("Enter the [green]flashcard[/] to update.")
                                                    .AddChoices(flashcardList.Select(s=>s.Question)));
                    int idInList = flashcardList.ToList().FindIndex(f => f.Question == flashcardQuestion);
                    if (idInList < flashcardList.Count() && idInList >= 0)
                    {
                        int id = flashcardList.ElementAt(idInList).Id;
                        await flashcardService.UpdateAsync(stackName, id);
                    }
                    else throw new Exception("Wrong id input.");
                }
                else throw new Exception("No stacks was found.");
            }
            catch (Exception ex)
            {
                Message(ex.Message);
            }
        }
            
        

        private async Task DeleteStack()
        {
            try
            {
                var list = await stackService.GetStackListAsync();
                if (list.Any())
                {
                    var name = AnsiConsole.Prompt(new SelectionPrompt<string>()
                                                    .Title("Enter stack name to [red]delete.[/]")
                                                    .AddChoices(list.Select(s => s.Name)));

                    if (AnsiConsole.Confirm("Are you sure?"))
                    {
                        await stackService.DeleteAsync(name);
                    }
                }
                else Message("No stacks was found.");
            }
            catch(Exception ex)
            {
                Message(ex.Message);
            }
        }

        private async Task DeleteFlashcard()
        {
            try
            {
                var stackList = await stackService.GetStackListAsync();
                if (stackList.Any())
                {
                    var stackName = AnsiConsole.Prompt(new SelectionPrompt<string>()
                                    .Title("Enter the [green]name of the stack[/] to which the flashcard belongs.")
                                    .AddChoices(stackList.Select(s => s.Name)));

                    var flashcardList = await flashcardService.GetListByStackNameAsync(stackName);

                    await ViewAllFlashcardsByStack(stackName, flashcardList);

                    var flashcardQuestion = AnsiConsole.Prompt(new SelectionPrompt<string>()
                                    .Title("Enter flashcard to [red]delete.[/]")
                                    .AddChoices(flashcardList.Select(s => s.Question)));

                    if (AnsiConsole.Confirm("Are you sure?"))
                    {
                        int idInList = flashcardList.ToList().FindIndex(f => f.Question == flashcardQuestion);
                        if (idInList < flashcardList.Count() && idInList >= 0)
                        {
                            int id = flashcardList.ElementAt(idInList).Id;
                            await flashcardService.DeleteAsync(id);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Message(ex.Message);
            }
        }

        private async Task Study()
        {
            try
            {
                if (AnsiConsole.Confirm("Start new study session?"))
                {
                    DateTime dateTime = DateTime.Now;
                    int score = 0;
                    var list = await stackService.GetStackListAsync();

                    await ViewAllStack(list);

                    if (list.Any())
                    {
                        var name = AnsiConsole.Prompt(
                                                new SelectionPrompt<string>()
                                                .Title("Enter stack name to [green]study.[/]")
                                                .AddChoices(list.Select(s => s.Name)));
                        var listOfFlashcards = await flashcardService.GetListByStackNameAsync(name);

                        if (listOfFlashcards.Any())
                        {
                            await ViewAllFlashcardsByStack(name, listOfFlashcards, showAnswer: false);
                            var selected = AnsiConsole.Prompt(
                                            new MultiSelectionPrompt<string>()
                                            .Title("Which [green]flashcards[/] do you want to study?")
                                            .AddChoices(listOfFlashcards.Select(s => s.Question).ToArray()));

                            for (int i = 0; i < selected.Count; i++)
                            {
                                var answer = AnsiConsole.Prompt(
                                         new TextPrompt<string>($"Enter answer to question: [green]{selected[i]}[/]"));
                                var correctAnswer = listOfFlashcards.Where(s => s.Question.ToLower() == selected[i].ToLower()).Select(s => s.Answer).First();
                                if (answer.ToLower().Equals(correctAnswer.ToLower()))
                                {
                                    AnsiConsole.MarkupLine($"Answer is [green]correct.[/]");
                                    score++;
                                }
                                else AnsiConsole.MarkupLine($"Answer is [red]incorrect.[/] Correct answer is " +
                                    $"[green]{correctAnswer}[/]");
                            }
                            Message($"Your study session is finished. Your total score: {score}");
                            await sessionService.CreateAsync(dateTime, score, name);
                        }
                        else Message("This stack has no flashcards.");
                    }
                    else Message("No stacks was found.");
                }
            }
            catch (Exception ex)
            {
                Message(ex.Message);
            }
        }

        private async Task ViewStudySessionsData()
        {
            try
            {
                AnsiConsole.Clear();
                var list = await sessionService.GetListAsync();
                if (list.Any())
                {
                    var table = new Table();
                    table.AddColumns("Date", "Time", "Score");
                    foreach (var item in list)
                    {
                        table.AddRow($"{item.Date}", $"{item.Time}", $"{item.Score}");
                    }
                    AnsiConsole.Write(table);
                    Message();
                }
                else
                {
                    Message("No session history was found.");
                }
            }
            catch (Exception ex)
            {  Message(ex.Message); }
        }

        private async Task ViewAllStack(IEnumerable<Stack>? list = null)
        {
            try
            {
                AnsiConsole.Clear();
                if (list == null)
                {
                    list = await stackService.GetStackListAsync();
                }
                if (list.Any())
                {
                    var table = new Table();
                    table.AddColumns("Id", "Name");
                    foreach (var item in list.OrderBy(s => s.Id))
                    {
                        table.AddRow($"{item.Id}", $"{item.Name}");
                    }
                    AnsiConsole.Write(table);
                }
                else
                {
                    Message("No stacks was found.");
                }
            }
            catch (Exception ex)
            {
                Message(ex.Message);
            }
        }

        private async Task ViewAllFlashcardsByStack(string? name = null, IEnumerable<FlashcardDto>? list = null, bool showAnswer=true)
        {
            try
            {
                AnsiConsole.Clear();
                var stackList = await stackService.GetStackListAsync();
                if (stackList.Any())
                {
                    if (name == null)
                    {
                        await ViewAllStack();

                        name = AnsiConsole.Prompt(
                                                new SelectionPrompt<string>()
                                                .Title("Enter stack name to [red]which the flashcard belongs.[/]")
                                                .AddChoices(stackList.Select(s => s.Name)));
                    }
                    if (list == null)
                    {
                        list = await flashcardService.GetListByStackNameAsync(name);
                    }
                    if (list.Any())
                    {
                        var table = new Table();
                        if (showAnswer)
                            table.AddColumns("Id", "Question", "Answer");
                        else table.AddColumns("Id", "Question");
                        int i = 1;
                        foreach (var item in list)
                        {
                            if (showAnswer)
                            {
                                table.AddRow($"{i}", $"{item.Question}", $"{item.Answer}");
                            }
                            else table.AddRow($"{i}", $"{item.Question}");
                            i++;
                        }
                        AnsiConsole.Write(table);
                        Message();
                    }
                    else
                    {
                        Message("No flashcards was found.");
                    }
                }
                else Message("No stacks was found.");
            }
            catch (Exception ex)
            {
                Message(ex.Message);
            }
        }

        private void Message(string? message = null)
        {
            if (message != null)
            {
                AnsiConsole.MarkupLine($"[red]{message}[/]");
            }
            AnsiConsole.MarkupLine("Press any key to continue...");
            Console.ReadKey();
        }

        private Task Exit()
        {
            Environment.Exit(0);
            return Task.CompletedTask;
        }
    }
}

