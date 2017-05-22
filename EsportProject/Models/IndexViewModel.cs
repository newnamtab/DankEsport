using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsportProject.Models
{
    public class IndexViewModel
    {
        public Models.FrontRankModel FrontRank { get; set; }

        public IndexViewModel()
        {
            this.FrontRank = new FrontRankModel();
            this.FrontRank.TournamentName = "AWESOME LAN!";
            Random ran = new Random();
            for (int i = 0; i < 5; i++)
            {
                this.FrontRank.Teams[i].TeamName = "Team " + ran.Next(10);
                this.FrontRank.Teams[i].Matches = ran.Next(10);
                this.FrontRank.Teams[i].Points = ran.Next(100);
            }
            this.FrontRank.Teams = this.FrontRank.Teams.OrderByDescending(item => item.Points).ToArray();
         
        }
    }
}
