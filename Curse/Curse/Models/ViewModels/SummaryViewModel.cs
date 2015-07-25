using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Curse.Models.ViewModels
{
    public class SummaryViewModel
    {
        public int TotalTeams { get; set; }
        public int TotalPlayers { get; set; }

        public PlayerViewModel PlayerWithMostRBI { get; set; }
        public PlayerViewModel PlayerWithMostHomeRuns { get; set; }
        public PlayerViewModel PlayerWithHighestAverage { get; set; }

        //future capability could be Teams with most etc.

        public bool HasAnyPlayers
        {
            get
            {
                bool hasplayers;

                hasplayers = PlayerWithMostRBI != null || PlayerWithMostHomeRuns != null || PlayerWithHighestAverage != null;

                return hasplayers;
            }
        }
    }
}