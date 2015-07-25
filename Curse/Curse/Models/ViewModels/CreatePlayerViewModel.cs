using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Curse.Models.ViewModels
{
    public class CreatePlayerViewModel
    {
        [Required]
        [MaxLength(100)]
        [Display(Name="Player Name")]
        public String Name { get; set; }

        [Display(Name = "RBI")]
        public int RBI { get; set; }

        [Display(Name = "Total Home Runs")]
        public int HomeRuns { get; set; }

        [Display(Name = "Batting Average")]
        public int Average { get; set; }

        [Display(Name="Team")]
        public int? TeamId { get; set; }

        public SelectList TeamSelectList { get; set; }
    }
}