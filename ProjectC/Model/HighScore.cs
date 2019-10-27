﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectC.Model
{
    public class HighScore : BaseModel
    {
        [PrimaryKey]
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public String UserName { get; set; }

        public Int32? Rank { get; set; }

        public Int32 Points { get; set; }

        public DateTimeOffset Date { get; set; }

        public HighScore()
        {
        }

        public HighScore(Guid userId, Int32 points, DateTimeOffset date)
        {
            this.UserId = userId;
            this.Points = points;
            this.Date = date;
        }
    }
}
