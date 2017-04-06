using System;
using Common.Logging;
using Common.Task;
using Quartz;
using TaskManager.Common.Log;
using TaskManager.Common.Log.Entity;
using TaskManager.Core;
using TaskManager.Task.Entities;
using TaskManager.Task.Services;

namespace TaskManager.Task.Quartz
{
    ///<summary>
    ///Quartz任务实现
    ///</summary>
    public class QuartzTask : IJob
    {
        ///<summary>
        ///执行任务
        ///</summary>
        ///<param name="context">Quartz任务运行环境</param>
        ///<remarks>外部不需调用，仅用于任务调度组建内部</remarks>
        public void Execute(IJobExecutionContext context)
        {
            int @int = context.JobDetail.JobDataMap.GetInt("Id");
            TaskDetailEntity task = TaskSchedulerFactory.GetScheduler().GetTask(@int);
            if (task == null)
            {
                throw new ArgumentException("Not found task ：" + @int);
            }
            TaskService service = new TaskService();
            task.IsRunning = true;
            DateTime utcNow = DateTime.UtcNow;
            service.SaveTaskStatus(task);
            try
            {
                ((ITask) Activator.CreateInstance(Type.GetType(task.ClassType))).Execute(task);
                task.LastIsSuccess = true;
            }
            catch (Exception e)
            {
                LogHelper<TaskMonitorEntity>.Error(new TaskMonitorEntity {TaskId = task.Id,Message = string.Format("Exception while running job {0} of type {1}", context.JobDetail.Key, context.JobDetail.JobType.ToString()) },e);
                task.LastIsSuccess = false;
            }
            task.IsRunning = false;
            task.LastStart = new DateTime?(utcNow);
            if (context.NextFireTimeUtc.HasValue)
            {
                task.NextStart = new DateTime?(context.NextFireTimeUtc.Value.UtcDateTime);
            }
            else
            {
                task.NextStart = null;
            }
            task.LastEnd = new DateTime?(DateTime.UtcNow);
            service.SaveTaskStatus(task);
        }
    }
}

