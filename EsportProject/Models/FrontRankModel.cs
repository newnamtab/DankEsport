using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsportProject.Models
{
    public class FrontRankModel
    {
        public string TournamentName { get; set; }
        public Classes.Teams[] Teams { get; set; }

        public FrontRankModel()
        {
            this.Teams = new Classes.Teams[5];
            for (int i = 0; i < 5; i++)
            {
                Teams[i] = new Classes.Teams();
            }
        }
    }
}
