using System;
using System.Collections.ObjectModel;

namespace ProjectC
{
    public class MultiPlayerHighScore
    {
        public int Rank { get; set; }
        public int Score { get; set; }
        public string User { get; set; }
    }

    public class SinglePlayerHighScore
    {
        public int Rank { get; set; }
        public int Score { get; set; }
        public DateTime Date { get; set; }
    }

    public class HighScoreDataService
    {
        public static ObservableCollection<MultiPlayerHighScore> GetMultiPlayerScores()
        {
            ObservableCollection<MultiPlayerHighScore> multiPlayerScores = new ObservableCollection<MultiPlayerHighScore>
            {
                new MultiPlayerHighScore { Rank = 1, Score = 10000, User = "User1" },
                new MultiPlayerHighScore { Rank = 2, Score = 9000, User = "User1" },
                new MultiPlayerHighScore { Rank = 3, Score = 8600, User = "User2" },
                new MultiPlayerHighScore { Rank = 4, Score = 8300, User = "User3" },
                new MultiPlayerHighScore { Rank = 5, Score = 8000, User = "User10" },
                new MultiPlayerHighScore { Rank = 6, Score = 7400, User = "User6" },
                new MultiPlayerHighScore { Rank = 7, Score = 7300, User = "User3" },
                new MultiPlayerHighScore { Rank = 8, Score = 7000, User = "User1" },
                new MultiPlayerHighScore { Rank = 9, Score = 6540, User = "User2" },
                new MultiPlayerHighScore { Rank = 10, Score = 6000, User = "User1" }
            };

            return multiPlayerScores;
        }

        public static ObservableCollection<SinglePlayerHighScore> GetSinglePlayerScores()
        {
            ObservableCollection<SinglePlayerHighScore> singlePlayerScores = new ObservableCollection<SinglePlayerHighScore>
            {
                new SinglePlayerHighScore { Rank = 1, Score = 10000, Date = new DateTime(2019, 10, 3) },
                new SinglePlayerHighScore { Rank = 2, Score = 9000, Date = new DateTime(2019, 9, 10) },
                new SinglePlayerHighScore { Rank = 3, Score = 8600, Date = new DateTime(2019, 10, 2) },
                new SinglePlayerHighScore { Rank = 4, Score = 8300, Date = new DateTime(2019, 8, 3) },
                new SinglePlayerHighScore { Rank = 5, Score = 8000, Date = new DateTime(2018, 10, 3) },
                new SinglePlayerHighScore { Rank = 6, Score = 7400, Date = new DateTime(2019, 7, 3) },
                new SinglePlayerHighScore { Rank = 7, Score = 7300, Date = new DateTime(2019, 5, 3) },
                new SinglePlayerHighScore { Rank = 8, Score = 7000, Date = new DateTime(2019, 8, 3) },
                new SinglePlayerHighScore { Rank = 9, Score = 6540, Date = new DateTime(2019, 4, 27) },
                new SinglePlayerHighScore { Rank = 10, Score = 6000, Date = new DateTime(2019, 6, 30) }
            };

            return singlePlayerScores;
        }

    }
}
