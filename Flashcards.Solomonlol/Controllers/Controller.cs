using Flashcards.Solomonlol.Data;

namespace Flashcards.Solomonlol.Controllers
{
    internal class Controller
    {
        UnitOfWork unitOfWork;
        public Controller()
        {
            unitOfWork = new UnitOfWork();
        }
    }
}
