using System;
using System.Collections.Generic;
using System.Linq;
using Common.Logging;
using Quartz;
using Quartz.Impl;
using QuartzExtention;
using QuartzExtention.Quartz;

namespace Infrastructure.Tasks.Quartz
{
    ///<summary>
    ///用以管理Quartz任务调度相关的操作
    ///</summary>
    ///2013/7/13 15:01:09  
    public class QuartzTaskScheduler : ITaskScheduler
    {
        private static List<TaskDetail> _taskDetails;

        ///<summary>
        ///构造器
        /// </summary>
        public QuartzTaskScheduler()
        {
            //todo _taskDetails读取
        }


        ///<summary>
        ///构造器,用于集群调用的
        /// </summary>
        public QuartzTaskScheduler(RunAtServer runAtServer) : this()
        {
            Func<TaskDetail, bool> predicate = null;
            if (_taskDetails != null)
            {
                predicate = n => n.RunAtServer == runAtServer;
                _taskDetails = _taskDetails.Where<TaskDetail>(predicate).ToList<TaskDetail>();
            }
        }
        /// <summary>
        /// 注销任务
        /// </summary>
        /// <param name="typeName"></param>
        private void DeleteJob(string typeName)
        {
            ISchedulerFactory factory = new StdSchedulerFactory();
            factory.GetScheduler().DeleteJob(new JobKey(typeName));
        }

        ///<summary>
        ///获取单个任务
        ///</summary>
        ///<param name="id">Id</param>
        ///<returns>任务详细信息</returns>
        public TaskDetail GetTask(int id)
        {
            return _taskDetails.FirstOrDefault<TaskDetail>(n => (n.Id == id));
        }

        ///<summary>
        ///重启所有任务
        ///</summary>
        public void ResumeAll()
        {
            this.Stop();
            this.Start();
        }

        ///<summary>
        ///运行单个任务
        ///</summary>
        ///<param name="id">任务Id</param>
        public void Run(int id)
        {
            TaskDetail task = this.GetTask(id);
            this.Run(task);
        }

        ///<summary>
        ///运行单个任务
        ///</summary>
        ///<param name="task">要运行的任务</param>
        public void Run(TaskDetail task)
        {
            if (task != null)
            {
                Type type = Type.GetType(task.ClassType);
                if (type == null)
                {
                    LogManager.GetLogger(this.GetType()).Error(message: string.Format("任务： {0} 的taskType为空。", task.Name));
                }
                else
                {
                    ITask task2 = (ITask) Activator.CreateInstance(type);
                    if ((task2 != null) && !task.IsRunning)
                    {
                        try
                        {
                            task2.Execute(null);
                        }
                        catch (Exception e)
                        {
                            LogManager.GetLogger(this.GetType()).Error(message: string.Format("执行任务： {0} 出现异常。", task.Name), exception: e);
                        }
                    }
                }
            }
        }

        ///<summary>
        ///保存任务状态
        ///</summary>
        ///<remarks>将当前需要需要ResumeContinue为true的任务记录，以便应用程序重启后检查是否需要立即执行</remarks>
        public void SaveTaskStatus()
        {
            foreach (TaskDetail detail in _taskDetails)
            {
                new TaskService().SaveTaskStatus(detail);
            }
        }

        ///<summary>
        ///启动任务
        ///</summary>
        public void Start()
        {
            if (_taskDetails.Count<TaskDetail>() != 0)
            {
                new TaskService();
                IScheduler scheduler = new StdSchedulerFactory().GetScheduler();
                foreach (TaskDetail detail in _taskDetails)
                {
                    if (detail.Enabled)
                    {
                        Type type = Type.GetType(detail.ClassType);
                        if (type != null)
                        {
                            string str = type.Name + "_trigger";
                            IJobDetail detail2 = JobBuilder.Create(typeof(QuartzTask)).WithIdentity(type.Name).Build();
                            detail2.JobDataMap.Add(new KeyValuePair<string, object>("Id", detail.Id));
                            TriggerBuilder builder = CronScheduleTriggerBuilderExtensions.WithCronSchedule(TriggerBuilder.Create().WithIdentity(str), detail.TaskRule);
                            if (detail.StartDate > DateTime.MinValue)
                            {
                                builder.StartAt(new DateTimeOffset(detail.StartDate));
                            }
                            DateTime? endDate = detail.EndDate;
                            DateTime startDate = detail.StartDate;
                            if (endDate.HasValue ? (endDate.GetValueOrDefault() > startDate) : false)
                            {
                                DateTime? nullable2 = detail.EndDate;
                                builder.EndAt(nullable2.HasValue ? new DateTimeOffset?(nullable2.GetValueOrDefault()) : null);
                            }
                            ICronTrigger trigger = (ICronTrigger) builder.Build();
                            DateTime dateTime = scheduler.ScheduleJob(detail2, trigger).DateTime;
                            detail.NextStart = new DateTime?(TimeZoneInfo.ConvertTime(dateTime, trigger.TimeZone));
                        }
                    }
                }
                scheduler.Start();
            }
        }

        ///<summary>
        ///停止任务
        ///</summary> 
        public void Stop()
        {
            ISchedulerFactory factory = new StdSchedulerFactory();
            factory.GetScheduler().Shutdown(true);
        }

        ///<summary>
        ///更新任务在调度器中的状态
        ///</summary>
        ///<param name="task">任务详细信息</param>
        public void Update(TaskDetail task)
        {
            if (task != null)
            {
                int num = _taskDetails.FindIndex(n => n.Id == task.Id);
                if (_taskDetails[num] != null)
                {
                    task.ClassType = _taskDetails[num].ClassType;
                    task.LastEnd = _taskDetails[num].LastEnd;
                    task.LastStart = _taskDetails[num].LastStart;
                    task.LastIsSuccess = _taskDetails[num].LastIsSuccess;
                    _taskDetails[num] = task;
                    Type type = Type.GetType(task.ClassType);
                    if (type != null)
                    {
                        this.DeleteJob(type.Name);
                        if (task.Enabled)
                        {
                            string str = type.Name + "_trigger";
                            IScheduler scheduler = new StdSchedulerFactory().GetScheduler();
                            IJobDetail detail = JobBuilder.Create(typeof(QuartzTask)).WithIdentity(type.Name).Build();
                            detail.JobDataMap.Add(new KeyValuePair<string, object>("Id", task.Id));
                            TriggerBuilder builder = CronScheduleTriggerBuilderExtensions.WithCronSchedule(TriggerBuilder.Create().WithIdentity(str), task.TaskRule);
                            if (task.StartDate > DateTime.MinValue)
                            {
                                builder.StartAt(new DateTimeOffset(task.StartDate));
                            }
                            if (task.EndDate.HasValue)
                            {
                                DateTime? endDate = task.EndDate;
                                DateTime startDate = task.StartDate;
                                if (endDate.HasValue ? (endDate.GetValueOrDefault() > startDate) : false)
                                {
                                    DateTime? nullable4 = task.EndDate;
                                    builder.EndAt(nullable4.HasValue ? new DateTimeOffset?(nullable4.GetValueOrDefault()) : null);
                                }
                            }
                            ICronTrigger trigger = (ICronTrigger) builder.Build();
                            DateTime dateTime = scheduler.ScheduleJob(detail, trigger).DateTime;
                            task.NextStart = new DateTime?(TimeZoneInfo.ConvertTime(dateTime, trigger.TimeZone));
                        }
                    }
                }
            }
        }
    }
}

