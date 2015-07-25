using Curse.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Curse.Helpers
{
    public static class DropDownHelper
    {
        //This is used to build a select list of Teams
        public static SelectList BuildTeamSelectList(ITeamRepository db)
        {
            var teams = db.FindAll().ToList().Select(x => new SelectListItem { Text = x.Name, Value = x.ID.ToString() });

            return new SelectList(teams, "Value", "Text");
        }
    }
}