using TaskManager.Task.Entities;

namespace TaskManager.Task
{
    ///<summary>
    ///用于注册任务的接口
    ///</summary>
    ///<remarks>所有任务都需要实现此接口</remarks>
    public interface ITask
    {
        ///<summary>
        ///执行任务的方法
        ///</summary>
        ///<param name=" taskDetail">任务配置状态信息</param>
        void Execute(TaskDetailEntity taskDetail = null);
    }
}

