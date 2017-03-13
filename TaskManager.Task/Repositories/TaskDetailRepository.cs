using PetaPoco;
using TaskManager.Common.PetaPoco;
using TaskManager.Task.Entities;

namespace TaskManager.Task.Repositories
{
    ///<summary>
    ///TaskDetailRepository
    ///</summary>
    public class TaskDetailRepository:Repository<TaskDetailEntity>
    {
        ///<summary>
        ///保存任务状态
        ///</summary>
        ///<param name="taskDetail">任务实体</param>
        public void SaveTaskStatus(TaskDetailEntity taskDetail)
        {
            Sql builder = Sql.Builder;
            builder.Append("update TaskDetail set LastStart = @0, LastEnd = @1,NextStart = @2,IsRunning = @3 where Id = @4", new object[] { taskDetail.LastStart, taskDetail.LastEnd, taskDetail.NextStart, taskDetail.IsRunning, taskDetail.Id });
            this.Db.Execute(builder);
        }
        /// <summary>
        /// 修改启用的状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="enabled"></param>
        public void ChangeEnabled(int id,bool enabled)
        {
            Sql sql = Sql.Builder;
            sql.Append("update TaskDetail set Enabled=@0 where Id=@1 ", enabled,id);
            this.Db.Execute(sql);
        }
    }
}

