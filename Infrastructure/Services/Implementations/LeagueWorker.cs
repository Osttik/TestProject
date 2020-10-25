using Model;
using Model.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Diagnostics.CodeAnalysis;
using Infrastructure.Services.Interfaces;

namespace Infrastructure.Services.Implementations
{
    public class LeagueWorker: ILeagueWorker
    {
        private IDataReader _dataReader;
        private List<League> _leagues;

        public LeagueWorker(IDataReader dataReader)
        {
            _dataReader = dataReader;
            _leagues = _dataReader.GetEnglishLeagues();
        }

        public List<Team> GetBestAttackerTeam()
        {
            return GetBestTeamsOn(_leagues, BestTeamCriteria.Attackers);
        }

        public List<Team> GetBestDefenderTeam()
        {
            return GetBestTeamsOn(_leagues, BestTeamCriteria.Defenders);
        }

        public List<Team> GetBestAttackToDefendTeam()
        {
            return GetBestTeamsOn(_leagues, BestTeamCriteria.ScoredToConceded);
        }

        public Team GetBestTeamFromLeagueOn(League league, BestTeamCriteria criteria)
        {
            Dictionary<string, Team> teams = new Dictionary<string, Team>();

            foreach (Match match in league.Matches)
            {
                if (match.Score == null)
                    continue;

                if (teams.ContainsKey(match.Team1))
                {
                    teams[match.Team1].GoalsScored += match.Score.Ft[0];
                    teams[match.Team1].ConcededGoals += match.Score.Ft[1];
                }
                else
                {
                    teams[match.Team1] = new Team(
                        match.Team1,
                        match.Score.Ft[0],
                        match.Score.Ft[1]);
                }

                if (teams.ContainsKey(match.Team2))
                {
                    teams[match.Team2].GoalsScored += match.Score.Ft[1];
                    teams[match.Team2].ConcededGoals += match.Score.Ft[0];
                }
                else
                {
                    teams[match.Team2] = new Team(
                        match.Team2,
                        match.Score.Ft[1],
                        match.Score.Ft[0]);
                }
            }

            Team bestTeam = teams.OrderByDescending(match => match.Value, new ComparerByGoals(criteria)).FirstOrDefault().Value;

            bestTeam.LeagueNumber = league.LeagueNumber;

            return bestTeam;
        }

        private class ComparerByGoals : IComparer<Team>
        {
            private BestTeamCriteria _currentCriteria;

            public ComparerByGoals(BestTeamCriteria criteria)
            {
                _currentCriteria = criteria;
            }

            public int Compare([AllowNull] Team x, [AllowNull] Team y)
            {
                if (_currentCriteria == BestTeamCriteria.Attackers)
                {
                    return x.GoalsScored.CompareTo(y.GoalsScored);
                }

                if (_currentCriteria == BestTeamCriteria.Defenders)
                {
                    return y.ConcededGoals.CompareTo(x.ConcededGoals);
                }

                int scoreX = x.GoalsScored;
                int scoreY = y.GoalsScored;

                if (scoreX <= x.ConcededGoals)
                    scoreX = 0;
                if (scoreY <= y.ConcededGoals)
                    scoreY = 0;

                return scoreX.CompareTo(scoreY);
            }
        }

        public List<Team> GetBestTeamsOn(List<League> leagues, BestTeamCriteria criteria)
        {
            List<Team> bestTeams = new List<Team>();

            foreach (League league in leagues)
            {
                Team bestTeam = GetBestTeamFromLeagueOn(league, criteria);

                bestTeams.Add(bestTeam);
            }

            return bestTeams;
        }

        public BestDay GetBestDay()
        {
            var answer = _leagues
                .SelectMany(league => league.Matches)
                .Where(match => match.Score != null)
                .GroupBy(match => match.Date)
                .Select(day => new BestDay(
                    day.Key,
                    day.Sum(match => match.Score.Ft[0] + match.Score.Ft[1])
                    ))
                .OrderByDescending(day => day.TotalGoals);

            return answer.FirstOrDefault();
        }
    }
}
