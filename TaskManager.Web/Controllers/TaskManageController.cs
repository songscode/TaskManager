using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using TaskManager.Task;
using TaskManager.Task.Entities;
using TaskManager.Task.Services;

namespace TaskManager.Web.Controllers
{
    public class TaskManageController : Controller
    {
        private TaskService _taskService=new TaskService();
        
        /// <summary>
        /// 任务列表
        /// </summary>
        /// <returns></returns>
        public ActionResult LocalTaskList()
        {
            var list = this._taskService.GetAll() as IList<TaskDetailEntity>;
            return View(list);
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
        /// <summary>
        /// 任务编辑
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult TaskDetailAddOrEdit(TaskDetailEntity entity)
        {
            try
            {
                if (entity.Id > 0)
                {
                    this._taskService.Update(entity);
                }
                else
                {
                    this._taskService.Insert(entity);
                }
                return Json(new { st = 1,msg="保存成功！" });
            }
            catch (Exception e)
            {
                return Json(new { st = 0, msg = "保存失败，"+e.Message });
            }
        }


        #region 任务执行操作
        /// <summary>
        /// 是否停用任务
        /// </summary>
        /// <param name="id"></param>
        /// <param name="enabled"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ChangeEnabled(int id, bool enabled)
        {
            this._taskService.ChangeEnabled(id, enabled);
            var entity = this._taskService.Get(id);
            return this.PartialView("_TaskDetailRow", entity);
        }
        /// <summary>
        /// 运行单个任务
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult RunTask(int id)
        {
            var taskScheduler = TaskSchedulerFactory.GetScheduler();
            taskScheduler.Run(id);
            return Json(new {st=1,msg="任务执行完成！"});
        }
        /// <summary>
        /// 重启所有任务
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ResumeAllTasks()
        {
            try
            {

                var list = this._taskService.GetAll() as IList<TaskDetailEntity>;
                //return View("LocalTaskList", list);
                var taskScheduler = TaskSchedulerFactory.GetScheduler();
                taskScheduler.ResumeAll();
                return View("LocalTaskList", list);
            }
            catch (Exception e)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, StringToISO_8859_1(e.Message));
            }
        }

        /// <summary>
        /// 转换为ISO_8859_1
        /// </summary>
        /// <param name="srcText"></param>
        /// <returns></returns>
        private string StringToISO_8859_1(string srcText)
        {
            string dst = "";
            char[] src = srcText.ToCharArray();
            for (int i = 0; i < src.Length; i++)
            {
                string str = @"&#" + (int)src[i] + ";";
                dst += str;
            }
            return dst;
        }
        /// <summary>
        /// 转换为原始字符串
        /// </summary>
        /// <param name="srcText"></param>
        /// <returns></returns>
        private string ISO_8859_1ToString(string srcText)
        {
            string dst = "";
            string[] src = srcText.Split(';');
            for (int i = 0; i < src.Length; i++)
            {
                if (src[i].Length > 0)
                {
                    string str = ((char)int.Parse(src[i].Substring(2))).ToString();
                    dst += str;
                }
            }
            return dst;
        }
        #endregion
    }
}