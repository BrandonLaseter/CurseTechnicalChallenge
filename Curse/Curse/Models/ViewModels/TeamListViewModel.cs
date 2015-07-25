using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Curse.Models.ViewModels
{
    public class TeamListViewModel
    {
        public int ID { get; set; }
        public String Name { get; set; }
        public int PlayerCount { get; set; }
    }
}