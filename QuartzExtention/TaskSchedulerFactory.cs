namespace TaskManager.Core
{
    public static class TaskSchedulerFactory
    {
        private static ITaskScheduler _taskScheduler;

        ///<summary>
        ///获取任务调度器
        ///</summary>
        ///<returns></returns>
        public static ITaskScheduler GetScheduler()
        {
            //if (a == null)
            //{
            //    a = DIContainer.Resolve<ITaskScheduler>();
            //}
            return _taskScheduler;
        }
    }
}

