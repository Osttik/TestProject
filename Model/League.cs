using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class League
    {
        public string Name { get; set; }
        public int LeagueNumber { get; set; }
        public List<Match> Matches { get; set; }
    }
}
