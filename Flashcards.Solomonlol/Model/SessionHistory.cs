namespace Flashcards.Solomonlol.Model
{
    internal class SessionHistory
    {
        public DateOnly Date {  get; set; }
        public TimeOnly Time { get; set; }
        public int Score { get; set; }
        public SessionHistory(DateOnly date, TimeOnly time, int score)
        {
            Date = date;
            Time = time;
            Score = score;
        }
    }
}
