using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectC.Model
{
    public class VMScore
    {
        public String UserName { get; set; }

        public Int32? Rank { get; set; }

        public Int32 Points { get; set; }

        public DateTimeOffset Date { get; set; }

        public VMScore(String userName, Int32? rank, Int32 points, DateTimeOffset date)
        {
            UserName = userName;
            Rank = rank;
            Points = points;
            Date = date;
        }
    }
}
