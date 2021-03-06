﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectC.Model
{
    public class Score : BaseModel
    {
        [PrimaryKey]
        public Guid Id { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset? ModifiedAt { get; set; }

        public Boolean Active { get; set; }

        public DateTimeOffset? LastSynchronized { get; set; }

        public Guid UserId { get; set; }

        public Int32 Points { get; set; }

        public DateTimeOffset Date { get; set; }

        public Boolean ManyLetters { get; set; }

        public String BestWord { get; set; }

        public Int32 BestWordValue { get; set; }

        public DifficultyEnum Difficulty { get; set; }

        public Score()
        {
        }

        public Score(Guid userId, Int32 points, DateTimeOffset date, Boolean manyLetters, String bestWord, Int32 bestWordValue, DifficultyEnum difficulty)
        {
            this.UserId = userId;
            this.Points = points;
            this.Date = date;
            this.ManyLetters = manyLetters;
            this.BestWord = bestWord;
            this.BestWordValue = bestWordValue;
            this.Difficulty = difficulty;
        }
    }
}
