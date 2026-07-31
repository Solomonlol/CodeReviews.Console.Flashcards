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
                { "Update stack by id", () => UpdateStack() },
                { "Delete stack by id", () => DeleteStack() }
            };
            _flashcardsMenu = new()
            {
                { "Back", () => Task.CompletedTask },
                { "View flashcards", () => ViewAllFlashcardsByStack() },
                { "Create flashcard", () => CreateFlashcard() },
                { "Update flashcard", () => UpdateFlashcard() }
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
            string name = AnsiConsole.Prompt(
                        new TextPrompt<string>("Enter stack [green]name[/]:"));
            await stackService.CreateAsync(name);
        }

        private async Task CreateFlashcard()
        {
            string stackName = AnsiConsole.Prompt(
                         new TextPrompt<string>("Enter the [green]name of the stack[/] to which the flashcard belongs."));
            string question = AnsiConsole.Prompt(
                        new TextPrompt<string>("Enter stack [green]Question[/]:"));
            string answer = AnsiConsole.Prompt(
                        new TextPrompt<string>("Enter stack [green]Answer[/]:"));
            
            await flashcardService.CreateAsync(stackName, question, answer);
        }


        private async Task UpdateStack()
        {
            string name = AnsiConsole.Prompt(
                         new TextPrompt<string>("Enter stack name to [red]update[/]"));
            if (AnsiConsole.Confirm("Are you shure?"))
            {
                await stackService.UpdateAsync(name);
            }
        }

        private async Task UpdateFlashcard()
        {
            try
            {
                await ViewAllStack();

                string stackName = AnsiConsole.Prompt(
                             new TextPrompt<string>("Enter stack name to [red]update[/]"));
                var check = await stackService.GetStackByNameAsync(stackName);
                var list = await flashcardService.GetListByStackNameAsync(stackName);

                await ViewAllFlashcardsByStack(stackName, list);

                int idInList = AnsiConsole.Prompt(
                         new TextPrompt<int>("Enter the [green]ID of the flashcard[/] to update."));

                if (idInList < list.Count() && idInList > 0)
                {
                    int id = list.ElementAt(idInList - 1).Id;
                    await flashcardService.UpdateAsync(stackName, id);
                }
                else throw new Exception("Wrong id input.");
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
                Console.ReadKey();
            }
        }
            
        

        private async Task DeleteStack()
        {
            string name = AnsiConsole.Prompt(
                         new TextPrompt<string>("Enter stack name to [red]delete[/]"));
            if (AnsiConsole.Confirm("Are you shure?"))
            {
                await stackService.DeleteAsync(name);
            }
        }

        private async Task Study()
        {
            await ViewAllStack();
        }

        private async Task ViewStudySessionsData()
        {

        }

        private async Task ViewAllStack()
        {
            AnsiConsole.Clear();
            var list = await stackService.GetStackListAsync();
            if (list.Any())
            {
                var table = new Table();
                table.AddColumns("Id", "Name");
                foreach (var item in list.OrderBy(s=>s.Id))
                {
                    table.AddRow($"{item.Id}", $"{item.Name}");
                }
                AnsiConsole.Write(table);
            }
            else
            {
                AnsiConsole.MarkupLine("[red]No stacks was found.[/]");
                AnsiConsole.MarkupLine("Press any key to continue...");
                Console.ReadKey();
            }
        }

        private async Task ViewAllFlashcardsByStack(string? name = null, IEnumerable<FlashcardDto>? list = null)
        {
            try
            {
                AnsiConsole.Clear();
                if (name == null)
                {
                    await ViewAllStack();
                    name = AnsiConsole.Prompt(
                                 new TextPrompt<string>("Enter stack name to [red]which the flashcard belongs.[/]"));
                }
                if (list == null)
                {
                    list = await flashcardService.GetListByStackNameAsync(name);
                }
                if (list.Any())
                {
                    var table = new Table();
                    table.AddColumns("Id", "Question", "Answer");
                    int i = 1;
                    foreach (var item in list)
                    {
                        table.AddRow($"{i}", $"{item.Question}", $"{item.Answer}");
                        i++;
                    }
                    AnsiConsole.Write(table);
                    AnsiConsole.MarkupLine("Press any key to continue...");
                    Console.ReadKey();
                }
                else
                {
                    AnsiConsole.MarkupLine("[red]No flashcards was found.[/]");
                    AnsiConsole.MarkupLine("Press any key to continue...");
                    Console.ReadKey();
                }
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
                Console.ReadKey();
            }
        }

        private Task Exit()
        {
            Environment.Exit(0);
            return Task.CompletedTask;
        }
    }
}

