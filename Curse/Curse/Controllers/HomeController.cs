using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Curse.Models;
using Curse.Models.Repository;
using Curse.Models.ViewModels;
using Curse.Helpers;

namespace Curse.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            try
            {
                ViewBag.Message = "Sports Stats Web Site.";
                ITeamRepository teamDb = new TeamRepository();
                IPlayerRepository playerDb = new PlayerRepository();

                //this viewmodel dipslays the "best" players for each stat and the total team and player count.
                var viewmodel = new SummaryViewModel
                {
                    TotalTeams = teamDb.FindAll().ToList().Count(),
                    TotalPlayers = playerDb.FindAll().ToList().Count(),                    
                };

                var mostRBI = playerDb.FindAll().OrderByDescending(x => x.RBI).FirstOrDefault();
                if (mostRBI != null && mostRBI.RBI > 0) viewmodel.PlayerWithMostRBI = mostRBI.ToPlayerViewModel();

                var mostHR = playerDb.FindAll().OrderByDescending(x => x.HomeRun).FirstOrDefault();
                if (mostHR != null && mostHR.HomeRun > 0) viewmodel.PlayerWithMostHomeRuns = mostHR.ToPlayerViewModel();

                var highestAvg = playerDb.FindAll().OrderByDescending(x => x.Average).FirstOrDefault();
                if (highestAvg != null && highestAvg.Average > 0) viewmodel.PlayerWithHighestAverage = highestAvg.ToPlayerViewModel();

                return View(viewmodel);
            }
            catch (Exception e)
            {
                //log error...
                return View(new SummaryViewModel());
            }
        }
    }
}
