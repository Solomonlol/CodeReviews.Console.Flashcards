using System.ComponentModel.DataAnnotations;

namespace Flashcards.Solomonlol.Model
{
    internal class Stack
    {
        [Key]
        public int Id {  get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;
        public ICollection<Flashcard> Flashcards { get; set; } = new List<Flashcard>();
        public ICollection<SessionHistory> Sessions { get; set; } = new List<SessionHistory>();
        public Stack()
        { }
        public Stack(string name)
        {
            Name = name;
        }
    }
}
