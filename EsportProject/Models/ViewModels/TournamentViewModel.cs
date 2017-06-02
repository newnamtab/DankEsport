using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EsportProject.Models.DBmodels;

namespace EsportProject.Models
{
    public class TournamentViewModel
    {
        public Turnament tournament { get; set; }
        public List<TeamPointsVM> TeamList { get; set; }
        public TournamentViewModel(Turnament tour)
        {
            tournament = tour;
            TeamList = new List<TeamPointsVM>();
        }
    }
}
