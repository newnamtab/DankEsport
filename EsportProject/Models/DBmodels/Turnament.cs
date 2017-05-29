using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsportProject.Models.DBmodels
{
    public class Turnament
    {
        public int TurnamentID { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime Slutdate { get; set; }
    }
}
