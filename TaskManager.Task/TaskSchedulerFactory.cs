using TaskManager.Common;

namespace TaskManager.Task
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
            if (_taskScheduler == null)
            {
                _taskScheduler = DIContainer.Resolve<ITaskScheduler>();
            }
            return _taskScheduler;
        }
    }
}

