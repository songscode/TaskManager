using System.Collections.Generic;
using TaskManager.Task.Entities;
using TaskManager.Task.Repositories;

namespace TaskManager.Task.Services
{
    ///<summary>
    ///任务业务逻辑
    ///</summary>
    public class TaskService
    {
        private TaskDetailRepository _taskDetailRepository;
        ///<summary>
        ///构造函数
        ///</summary>
        public TaskService() 
        {
            _taskDetailRepository=new TaskDetailRepository();
        }

        ///<summary>
        ///依据TaskName获取任务
        ///</summary>
        ///<param name="Id">任务Id</param>
        public TaskDetailEntity Get(int Id)
        {
            return this._taskDetailRepository.Get(Id);
        }

        ///<summary>
        ///获取所用任务
        ///</summary>
        ///<returns></returns>
        public IEnumerable<TaskDetailEntity> GetAll()
        {
            return this._taskDetailRepository.GetAll();
        }

        ///<summary>
        ///应用程序关闭时保存任务当前状态
        ///</summary>
        ///<param name="entity">任务详细信息实体</param>
        public void SaveTaskStatus(TaskDetailEntity entity)
        {
            this._taskDetailRepository.SaveTaskStatus(entity);
        }

        ///<summary>
        ///更新任务相关信息
        ///</summary>
        ///<param name="entity">任务详细信息实体</param>
        public void Update(TaskDetailEntity entity)
        {
            TaskSchedulerFactory.GetScheduler().Update(entity);
            //更新任务
            this._taskDetailRepository.Update(entity);
        }
        /// <summary>
        /// 新增任务相关信息
        /// </summary>
        /// <param name="entity"></param>
        public void Insert(TaskDetailEntity entity)
        {
            this._taskDetailRepository.Insert(entity);
        }
        /// <summary>
        /// 修改启用的状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="enabled"></param>
        public void ChangeEnabled(int id,bool enabled)
        {
            this._taskDetailRepository.ChangeEnabled(id,enabled);
        }
    }
}

