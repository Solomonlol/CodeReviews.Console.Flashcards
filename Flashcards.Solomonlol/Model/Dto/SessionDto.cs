using System;
using System.Collections.Generic;
using System.Text;

namespace Flashcards.Solomonlol.Model.Dto
{
    internal class SessionDto
    {
        public DateOnly Date {  get; set; }
        public TimeOnly Time { get; set; }
        public int Score { get; set; }

        public SessionDto()
        { }

        public SessionDto(SessionHistory item)
        { 
            Date = DateOnly.FromDateTime(item.Date);
            Time = TimeOnly.FromDateTime(item.Date);
            Score = item.Score;
        }
    }
}
