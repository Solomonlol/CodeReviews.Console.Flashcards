using Spectre.Console;
using static Flashcards.Solomonlol.Controllers.MainMenuList;

namespace Flashcards.Solomonlol.Controllers
{
    internal class MainMenu
    {
        //MainMenuList menu = new MainMenuList();

        public static void Menu()
        {
            while (true)
            {
                ViewMenu(menuList);
            }
        }

        private static void ViewMenu(Dictionary<string, Action> dictionary)
        {
            var choice = AnsiConsole.Prompt(new SelectionPrompt<string>()
                    .Title("Select an [green]option[/]:")
                    .AddChoices(dictionary.Keys));

            dictionary[choice]();
        }
    }
}

