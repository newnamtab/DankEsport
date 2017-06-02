using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EsportProject.Models.DBmodels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EsportProject.Models
{
    public class TeamTurnamentVM
    {
        private readonly TurnamentContext _context;
        public List<SelectListItem> TeamsList { get; set; }
        public List<SelectListItem> TurnamentList;

        public TeamTurnamentVM(TurnamentContext context)
        {
            _context = context;
            //List<Team> teamList = _context.Team.ToList();
            List<Team> teamlist = _context.Team.ToList();
            List<Turnament> turnamentList = _context.Turnament.ToList();
            SetLists(turnamentList, teamlist);
        }
        public void SetLists(List<Turnament> turns, List<Team> teams)
        {
            TurnamentList = new List<SelectListItem>();
            TeamsList = new List<SelectListItem>();
            foreach (Team tm in teams)
            {
               TeamsList.Add(new SelectListItem { Value = tm.TeamID.ToString(), Text = tm.Name });
            }
            foreach (Turnament tur in turns)
            {
                TurnamentList.Add(new SelectListItem { Value = tur.TurnamentID.ToString(), Text = tur.Name });
            }
        }
    }
}
