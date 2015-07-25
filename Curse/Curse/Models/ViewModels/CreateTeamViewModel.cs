using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Curse.Models.ViewModels
{
    public class CreateTeamViewModel
    {
        [Required]
        [MaxLength(100)]
        public String Name { get; set; }

        //not sure this will work
        //public HttpPostedFileBase Avatar { get; set; }
    }
}