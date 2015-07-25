using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Curse.Models.ViewModels
{
    public class PlayerListViewModel
    {
        public int ID { get; set; }
        public String Name { get; set; }

        public int RBI { get; set; }
        public int Homerun { get; set; }
        public int Average { get; set; }

        public String TeamName { get; set; }
        public int? TeamId { get; set; }
    }
}