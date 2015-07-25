using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Curse.Models.ViewModels
{
    public class TeamViewModel
    {
        [Required]
        public int ID { get; set; }

        [Required]
        [MaxLength(100)]
        public String Name { get; set; }

        public List<PlayerViewModel> PlayerList { get; set; }

        public String Avatar { get; set; }
    }
}