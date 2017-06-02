using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EsportProject.Models.DBmodels;

namespace EsportProject.Models
{
    public class TeamPointsVM
    {
        public Team Team { get; set; }
        public Turnament Tournament { get; set; }
        public int WonMatches { get; set; }
        public int drawMatches { get; set; }
        public int LostMatches { get; set; }
        public int Points { get; set; }
        public int totalMatches { get; set; }
        public TeamPointsVM(TeamStanding ts)
        {
            Team = ts.Team;
            Tournament = ts.Turnament;
            WonMatches = ts.WonMatches;
            drawMatches = ts.drawMatches;
            LostMatches = ts.LostMatches;
            Points = ts.Points();
            totalMatches = ts.totalMatches();
        }
    }
}
