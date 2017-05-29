using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace EsportProject.Models.DBmodels
{
    public class TurnamentContext : DbContext
    {
        public TurnamentContext(DbContextOptions<TurnamentContext> options)
            : base(options)
        { }
        public DbSet<Turnament> Turnament { get; set; }
        public DbSet<Team> Team { get; set; }
        public DbSet<TeamStanding> TeamStanding { get; set; }
    }
}
