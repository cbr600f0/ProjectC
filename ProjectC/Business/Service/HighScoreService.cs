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
    public class ScoreService : BaseService<Score>
    {
        private UserService _userService;
        protected UserService UserService
        {
            get
            {
                return this._userService = this._userService ?? new UserService();
            }
        }

        public ScoreService() : base()
        {
        }

        public List<Score> Get()
        {
            return base.Get<Score>().ToList();
        }

        public Score Get(Guid id)
        {
            return base.Get<Score>(id).SingleOrDefault();
        }

        public void Delete(Guid id)
        {
            base.Delete<Score>(id);
        }

        public void AddOrUpdate(Score score)
        {
            score.UserName = this.UserService.Get(score.UserId).UserName;
            try
            {
                base.AddOrUpdate<Score>(ref score);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Score> GetByUserId(Guid userId)
        {
            return base.Get<Score>()
                .Where(hs => hs.UserId == userId)
                .OrderByDescending(hs => hs.Points)
                .ToList();
        }

        public List<Score> GetRankedScores(Boolean isLocal)
        {

            List<Score> scores = isLocal ? this.GetByUserId(base.CurrentUserId.Value)
                .OrderByDescending(hs => hs.Points)
                .Take(10)
                .ToList()
                : this.Get()
                .OrderByDescending(hs => hs.Points)
                .Take(10)
                .ToList();

            Int32 rank = 1;

            foreach (Score score in scores)
            {
                score.Rank = rank;
                rank++;
            }
            return scores;
        }
    }
}
