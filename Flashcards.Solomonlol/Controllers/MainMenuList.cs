using System;
using System.Collections.Generic;
using System.Text;

namespace Flashcards.Solomonlol.Controllers
{
    public class MainMenuList
    {
        public Dictionary<string, Action> menuList = new()
        {
            { "Return", () => Console.WriteLine("") },
            { "Return", () => Console.WriteLine("") }
        };
}
}

