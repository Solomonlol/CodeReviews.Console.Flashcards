using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flashcards.Solomonlol.Model
{
    internal class SessionHistory
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date {  get; set; }
        public int Score { get; set; }
        public int StackID { get; set; }

        [ForeignKey(nameof(StackID))]
        public Stack Stack { get; set; } = null!;
        public SessionHistory()
        { }
        public SessionHistory(DateTime dateTime, int score, int stackId)
        {
            Date = dateTime;
            Score = score;
            StackID = stackId;
        }
    }
}
