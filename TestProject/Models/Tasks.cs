using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestProject.Models
{
    public class Tasks
    {
        public List<Team> AttackTeams { get; set; }
        public List<Team> DefendTeams { get; set; }
        public List<Team> AttackToDefendTeams { get; set; }
        public BestDay BestDay { get; set; }
    }
}
