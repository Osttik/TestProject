using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Model
{
    public class Team
    {
        public string Name { get; set; }
        public int GoalsScored { get; set; }
        public int ConcededGoals { get; set; }
        public int LeagueNumber { get; set; }

        public Team()
        {
            Name = "Unknown";
            GoalsScored = 0;
            ConcededGoals = 0;
            LeagueNumber = -1;
        }

        public Team(string name, int goalsScored, int concededGoals): base()
        {
            Name = name;
            GoalsScored = goalsScored;
            ConcededGoals = concededGoals;
        }
    }
}
