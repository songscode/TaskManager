using System;
using System.Collections.Generic;
using Infrastructure.Tasks;

namespace QuartzExtention
{
    ///<summary>
    ///任务业务逻辑
    ///</summary>
    public class TaskService
    {
        ///<summary>
        ///构造函数
        ///</summary>
        public TaskService() 
        {
        }

        ///<summary>
        ///依据TaskName获取任务
        ///</summary>
        ///<param name="Id">任务Id</param>
        public TaskDetail Get(int Id)
        {
            //return this.a.Get(Id);
            throw new NotImplementedException();
        }

        ///<summary>
        ///获取所用任务
        ///</summary>
        ///<returns></returns>
        public IEnumerable<TaskDetail> GetAll()
        {
            throw new NotImplementedException();
        }

        ///<summary>
        ///应用程序关闭时保存任务当前状态
        ///</summary>
        ///<param name="entity">任务详细信息实体</param>
        public void SaveTaskStatus(TaskDetail entity)
        {
            throw new NotImplementedException();
        }

        ///<summary>
        ///更新任务相关信息
        ///</summary>
        ///<param name="entity">任务详细信息实体</param>
        ///2013/7/13 15:01:09  
        public void Update(TaskDetail entity)
        {
            TaskSchedulerFactory.GetScheduler().Update(entity);
            //更新任务
            //？
        }
    }
}

