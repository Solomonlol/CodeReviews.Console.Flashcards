namespace Flashcards.Solomonlol.Controllers
{
    public class MainMenuList
    {
        
        //StackRepository repository = new(ApplicationContext context = new());
        public static Dictionary<string, Action> menuList = new()
        {
            { "Exit", () => Exit() },
            { "View study sessions", () => Console.WriteLine("Study Sessions") },
            { "Study", ()=>Console.WriteLine("Study...") },
            { "Manage stacks", ()=>Console.WriteLine("Stacks") },
            { "Manage flashcards", ()=>Console.WriteLine("Flashcards") }
        };

        private static void Exit()
        {
            Environment.Exit(0);
        }

        private static void ViewStudySessions()
        {
            
        }
        private static void ViewStacks()
        {

        }
        private static void CreateStack()
        {

        }

    }
}

