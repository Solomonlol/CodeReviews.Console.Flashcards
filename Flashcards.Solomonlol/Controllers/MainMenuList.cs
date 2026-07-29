using Flashcards.Solomonlol.Services;
using Spectre.Console;

namespace Flashcards.Solomonlol.Controllers
{
    public class MainMenuList
    {
        
        public Dictionary<string, Action> MainMenu {  get; set; }
        public Dictionary<string, Action> StackMenu { get; set; }
        public Dictionary<string, Action> FlashcardsMenu { get; set; }
        //public Dictionary<string, Action> SessionsMenu { get; set; }

        public MainMenuList(Dictionary<string, Action> main, Dictionary<string, Action> stack, Dictionary<string, Action> flashcards)
        {
            MainMenu = main;
            StackMenu = stack;
            FlashcardsMenu = flashcards;
            //SessionsMenu = sessions;
        }
    }
}

