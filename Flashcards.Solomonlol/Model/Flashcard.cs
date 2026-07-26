using System.ComponentModel.DataAnnotations.Schema;

namespace Flashcards.Solomonlol.Model
{
    internal class Flashcard
    {
        public int? Id { get; set; } = null;
        public string Question { get; set; }
        public string Answer { get; set; }

        public int StackID { get; set; }

        [ForeignKey(nameof(StackID))]
        public Stack Stack { get; set; }
    }
}
