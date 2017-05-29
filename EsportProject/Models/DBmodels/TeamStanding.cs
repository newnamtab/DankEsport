using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsportProject.Models.DBmodels
{
    public class TeamStanding
    {
        public int TeamStandingID { get; set; }
        public Team Team { get; set; }
        public Turnament Turnament { get; set; }
        public int WonMatches { get; set; }
        public int drawMatches { get; set; }
        public int LostMatches { get; set; }
        public int Points()
        {
            return WonMatches * 3 + drawMatches * 2;
        }
        public int totalMatches()
        {
            return WonMatches + drawMatches + LostMatches;
        }
    }
}
