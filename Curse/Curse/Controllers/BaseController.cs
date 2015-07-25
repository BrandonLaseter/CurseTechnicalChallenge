using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Curse.Controllers
{
    public class BaseController : Controller
    {
        protected const String GenericErrorMessage = "Exception occured on the server";
        protected const String GenericDatabaseError = "Database error has occurred on the server";
    }
}
