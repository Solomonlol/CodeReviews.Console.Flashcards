namespace Flashcards.Solomonlol.Model
{
    internal class Stack
    {
        public List<Flashcard>? flashcards;
        public List<SessionHistory>? sessions;
        public int Id {  get; set; }
        public string StackName { get; set; }

    }
}
