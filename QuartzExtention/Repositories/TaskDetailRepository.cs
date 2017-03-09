using MySql.Data.MySqlClient;
using PetaPoco;
using TaskManager.Common.PetaPoco;
using TaskManager.Core.Entities;

namespace TaskManager.Core.Repositories
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
        
    }
}

