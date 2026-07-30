using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flashcards.Solomonlol.Model
{
    internal class Flashcard
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Question { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Answer { get; set; } = string.Empty;

        public int StackID { get; set; }

        [ForeignKey(nameof(StackID))]
        public Stack Stack { get; set; }
    }
}
