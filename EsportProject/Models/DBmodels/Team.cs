using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsportProject.Models.DBmodels
{
    public class Team
    {
        public int TeamID { get; set; }
        public string Name { get; set; }
        public string ShorntenedName { get; set; }
        public string Spillere
        {
            get; set;
        }
    }
}
