using System;
using System.Collections.Generic;
using System.Text;

namespace Flashcards.Solomonlol.Dto
{
    internal class SessionDto
    {
        public DateOnly Date {  get; set; }
        public TimeOnly Time { get; set; }
        public int Score { get; set; }
    }
}
