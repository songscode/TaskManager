using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManager.Task.Entities;
using TaskManager.Task.Services;

namespace TaskManager.Web.Controllers
{
    public class TaskManageController : Controller
    {
        private TaskService _taskService=new TaskService();
        // GET: TaskDetails
        public ActionResult LocalTaskList()
        {
            return View();
        }

        public ActionResult TaskDetailAddOrEdit(int id=0)
        {
            TaskDetailEntity entity=new TaskDetailEntity();
            if (id > 0)
            {
                entity = this._taskService.Get(id);
            }
            return View(entity);
        }

        [HttpPost]
        public ActionResult TaskDetailAddOrEdit(TaskDetailEntity entity)
        {
            return Json(new {st=1});
        }
    }
}