using Flashcards.Solomonlol.Services;
using Spectre.Console;

namespace Flashcards.Solomonlol.Controllers
{
    internal class MainMenu
    {
        private readonly StackService stackService = new();
        private readonly Dictionary<string, Func<Task>> _mainMenu;
        private readonly Dictionary<string, Func<Task>> _stackMenu;
        private readonly Dictionary<string, Func<Task>> _flashcardsMenu;
        public MainMenu()
        {
            _mainMenu = new()
            {
                { "Exit", () => Exit() },
                //{ "View study sessions", () => ViewStudySessions() },
                //{ "Study", ()=> Study() },
                { "Manage stacks", ()=> ViewSubMenu(_stackMenu, "Stack menu") },
                { "Manage flashcards", ()=>ViewSubMenu(_flashcardsMenu, "Flashcards menu") }
            };
            _stackMenu = new()
            {
                { "Back", () => Task.CompletedTask },
                { "View stack list", () => ViewAllStack() },
                { "Create new stack", () => CreateStack() },
                //{ "Update stack by id", () => Console.WriteLine("Study Sessions") },
                //{ "Delete stack by id", () => Console.WriteLine("Study Sessions") }
            };
            _flashcardsMenu = new()
            {
                { "Back", () => Task.CompletedTask }
                 
            };
        }
        public async Task Menu()
        {
            while (true)
            {
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
                        new TextPrompt<string>("Enter stack [green]name[/]?"));
            await stackService.CreateAsync(name);
            await stackService.SaveAsync();
        }

        private async Task ViewAllStack()
        {
            var list = await stackService.GetStackListAsync();
            var table = new Table();
            table.AddColumns("Id", "Name");
            foreach (var item in list)
            {
                table.AddRow($"{item.Id}", $"{item.StackName}");
            }
            AnsiConsole.Write(table);
        }
        private Task Exit()
        {
            Environment.Exit(0);
            return Task.CompletedTask;
        }
    }
}

