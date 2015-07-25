using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Curse.Models.ViewModels
{
    public class PlayerViewModel//I possibly could have derived tis from CreatePlayerViewModel
    {
        [Required]
        public int ID { get; set; }

        [Required]
        [MaxLength(100)]
        public String Name { get; set; }

        [Display(Name = "RBI")]
        public int RBI { get; set; }

        [Display(Name = "Total Home Runs")]
        public int HomeRuns { get; set; }

        [Display(Name = "Batting Average")]
        public int Average { get; set; }

        [Display(Name="Team")]
        public int? TeamId { get; set; }

        [Display(Name = "Team")]
        public String TeamName { get; set; }

        public SelectList TeamSelectList { get; set; }

        [Display(Name="Avatar")]
        public String Avatar { get; set; }
    }
}