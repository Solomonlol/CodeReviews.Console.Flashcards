using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace Flashcards.Solomonlol.Controllers
{
    internal class MainMenu
    {
        public static void Menu()
        {
            MainMenuList list = new();
            while (true)
            {
                var choice = AnsiConsole.Prompt(new SelectionPrompt<string>()
                    .Title("Select an [green]option[/]:")
                    .AddChoices(list.menuList.Keys));

                list.menuList[choice]();
            }
        }
    }
}

