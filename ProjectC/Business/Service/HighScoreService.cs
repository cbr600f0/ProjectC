using ProjectC.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using SQLite;
using Xamarin.Forms;
using ProjectC.Business.Interface;

namespace ProjectC.Business.Service
{
    public class HighScoreService : BaseService<HighScore>
    {
        private UserService _userService;
        protected UserService UserService
        {
            get
            {
                return this._userService = this._userService ?? new UserService();
            }
        }

        public HighScoreService() : base()
        {
        }

        public List<HighScore> Get()
        {
            return base.Get<HighScore>().ToList();
        }

        public HighScore Get(Guid id)
        {
            return base.Get<HighScore>(id).SingleOrDefault();
        }

        public void Delete(Guid id)
        {
            base.Delete<HighScore>(id);
        }

        public void AddOrUpdate(HighScore highscore)
        {
            highscore.UserName = this.UserService.Get(highscore.UserId).UserName;
            try
            {
                base.AddOrUpdate<HighScore>(ref highscore);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<HighScore> GetByUserId(Guid userId)
        {
            return base.Get<HighScore>()
                .Where(hs => hs.UserId == userId)
                .OrderByDescending(hs => hs.Points)
                .ToList();
        }

        public List<HighScore> GetRankedHighScores(Boolean isLocal)
        {

            List<HighScore> highScores = isLocal ? this.GetByUserId(base.CurrentUserId.Value)
                .OrderByDescending(hs => hs.Points)
                .Take(10)
                .ToList()
                : this.Get()
                .OrderByDescending(hs => hs.Points)
                .Take(10)
                .ToList();

            Int32 rank = 1;

            foreach (HighScore highScore in highScores)
            {
                highScore.Rank = rank;
                rank++;
            }
            return highScores;
        }
    }
}
