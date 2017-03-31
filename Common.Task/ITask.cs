namespace Common.Task
{
    ///<summary>
    ///用于注册任务的接口
    ///</summary>
    ///<remarks>所有任务都需要实现此接口</remarks>
    public interface ITask
    {
        /// <summary>
        /// 执行任务的方法
        /// </summary>
        /// <param name="context">任务配置参数</param>
        void Execute(ITaskExecutionContext context = null);
    }
}

