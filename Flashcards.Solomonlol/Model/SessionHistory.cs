using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flashcards.Solomonlol.Model
{
    internal class SessionHistory
    {
        [Key]
        public int? Id { get; set; } = null;
        public DateOnly Date {  get; set; }
        public TimeOnly Time { get; set; }
        public int Score { get; set; }
        public int StackID { get; set; }

        [ForeignKey(nameof(StackID))]
        public Stack Stack { get; set; } = null!;
        public SessionHistory()
        { }
        public SessionHistory(DateOnly date, TimeOnly time, int score)
        {
            Date = date;
            Time = time;
            Score = score;
        }
    }
}
