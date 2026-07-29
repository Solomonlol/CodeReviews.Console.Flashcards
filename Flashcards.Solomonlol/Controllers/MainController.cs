using Flashcards.Solomonlol.Data;

namespace Flashcards.Solomonlol.Controllers
{
    internal class MainController
    {
        UnitOfWork unitOfWork;
        public MainController()
        {
            unitOfWork = new UnitOfWork();
        }


    }
}
