using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TaskManager.Web.Controllers
{
    public class LocalTaskController : Controller
    {
        // GET: TaskDetails
        public ActionResult TaskDetails()
        {
            return View();
        }
    }
}