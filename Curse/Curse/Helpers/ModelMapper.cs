using Curse.Models;
using Curse.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Curse.Helpers
{
    public static class ModelMapper
    {
        /// <summary>
        /// This is an extension method on the Player class for mapping a Player object to a PlayerViewModel object
        /// </summary>
        /// <param name="model">The Player object</param>
        /// <returns>A new PlayerViewModel object</returns>
        public static PlayerViewModel ToPlayerViewModel(this Player model)
        {
            if (model == null) return null;

            return new PlayerViewModel()
            {
                ID = model.ID,
                Name = model.Name,
                TeamId = model.TeamId,
                TeamName = model.Team != null ? model.Team.Name : null,                
                RBI = model.RBI,
                HomeRuns = model.HomeRun,
                Average = model.Average
            };
        }        
    }
}