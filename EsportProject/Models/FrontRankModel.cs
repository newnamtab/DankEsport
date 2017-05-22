using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsportProject.Models
{
    public class FrontRankModel
    {
        public string TournamentName { get; set; }
        public Classes.Team[] Teams { get; set; }

        public FrontRankModel()
        {
            this.Teams = new Classes.Team[5];
            for (int i = 0; i < 5; i++)
            {
                Teams[i] = new Classes.Team();
            }
        }
    }
}
