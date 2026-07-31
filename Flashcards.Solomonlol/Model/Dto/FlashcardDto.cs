using System;
using System.Collections.Generic;
using System.Text;

namespace Flashcards.Solomonlol.Model.Dto
{
    internal class FlashcardDto
    {
        public int Id { get; set; }
        public string Question {  get; set; }
        public string Answer { get; set; } = string.Empty;

        public FlashcardDto(string question, string answer)
        {
            Question = question;
            Answer = answer;
        }
        public FlashcardDto(Flashcard flashcard)
        {
            this.Id = flashcard.Id;
            this.Question = flashcard.Question;
            this.Answer = flashcard.Answer;
        }
    }
}
