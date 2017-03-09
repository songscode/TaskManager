using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TaskManager.Web.Controllers
{
    public class TaskManageController : Controller
    {
        // GET: TaskDetails
        public ActionResult LocalTaskList()
        {
            return View();
        }
    }
}