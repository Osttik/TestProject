using Model;
using Model.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Services.Interfaces
{
    public interface ILeagueWorker
    {
        public List<Team> GetBestTeamsOn(List<League> leagues, BestTeamCriteria criteria);
        public List<Team> GetBestAttackerTeam();
        public List<Team> GetBestDefenderTeam();
        public List<Team> GetBestAttackToDefendTeam();
        public BestDay GetBestDay();
    }
}
