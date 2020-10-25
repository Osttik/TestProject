using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Services.Implementations;
using Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TestProject.Models;

namespace TestProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ILeagueWorker _leagueWorker;

        public HomeController(ILogger<HomeController> logger, ILeagueWorker leagueWorker)
        {
            _logger = logger;
            _leagueWorker = leagueWorker;
        }

        public IActionResult Index()
        {
            Tasks tasks = new Tasks();
            tasks.AttackTeams = _leagueWorker.GetBestAttackerTeam();
            tasks.DefendTeams = _leagueWorker.GetBestDefenderTeam();
            tasks.AttackToDefendTeams = _leagueWorker.GetBestAttackToDefendTeam();
            tasks.BestDay = _leagueWorker.GetBestDay();

            return View(tasks);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
