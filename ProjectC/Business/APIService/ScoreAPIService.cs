using ProjectC.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using SQLite;
using Xamarin.Forms;
using ProjectC.Business.Interface;
using ProjectC.Business.Service;
using Plugin.Connectivity;

namespace ProjectC.Business.APIService
{
    public class ScoreAPIService : BaseAPIService<Score>
    {
        public ScoreAPIService() : base()
        {
        }

        public List<Score> Get()
        {
            if(base.ApiIsAvailable())
            {
                return base.Get<Score>().ToList();
            }
            else
            {
                return base.ScoreService.Get();
            }
        }

        public Score Get(Guid id)
        {
            return base.Get<Score>(id).SingleOrDefault();
        }

        public void AddOrUpdate(Score score)
        {
            if (base.ApiIsAvailable())
            {
                score.Id = base.AddOrUpdate<Score>(ref score);
                score.LastSynchronized = DateTimeOffset.Now;
            }
            base.ScoreService.AddOrUpdate(score);
        }

        public List<VMScore> GetRankedScores()
        {
            List<VMScore> vmScores = new List<VMScore>();

            List<Score> scores = this.Get()
                .OrderByDescending(s => s.Points)
                .ToList();

            Int32 rank = 1;

            foreach (Score score in scores)
            {
                vmScores.Add(new VMScore(base.UserService.Get(score.UserId).UserName, rank, score.Points, score.Date));
                rank++;
            }
            return vmScores;
        }
    }
}
