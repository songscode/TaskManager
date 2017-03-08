namespace QuartzExtention
{
    ///<summary>
    ///任务执行控制器
    ///</summary>
    ///<remarks>需要从DI容器中获取注册的tasks</remarks>
    public interface ITaskScheduler
    {
        ///<summary>
        ///获取单个任务
        ///</summary>
        ///<param name="id">任务Id</param>
        ///<returns>任务详细信息</returns>
        TaskDetail GetTask(int id);
        ///<summary>
        ///重启所有任务
        ///</summary>
        void ResumeAll();
        ///<summary>
        ///执行单个任务
        ///</summary>
        ///<param name="id">任务Id</param>
        void Run(int id);
        ///<summary>
        ///保存任务状态
        ///</summary>
        ///<remarks>将当前需要需要ResumeContinue为true的任务记录，以便应用程序重启后检查是否需要立即执行</remarks>
        void SaveTaskStatus();
        ///<summary>
        ///开始执行任务
        ///</summary>
        void Start();
        ///<summary>
        ///停止任务
        ///</summary>
        void Stop();
        ///<summary>
        ///更新任务在调度器中的状态
        ///</summary>
        ///<param name="task">任务详细信息</param>
        void Update(TaskDetail task);
    }
}

