using System;
using System.Collections.Specialized;
using Common.Task;
using PetaPoco;
using TaskManager.Common.PetaPoco;

namespace TaskManager.Task.Entities
{
    [Serializable]
    [TableName("taskdetail")]
    [PrimaryKey("id")]
    public class TaskDetailEntity : TaskExecutionContextImpl
    {
        ///<summary>
        ///获取规则指定部分
        ///</summary>
        ///<param name="rulePart">规则组成部分</param>
        ///<returns></returns>
        public string GetRulePart(RulePart rulePart)
        {
            if (string.IsNullOrEmpty(this.TaskRule))
            {
                if (RulePart.DayOfWeek != rulePart)
                {
                    return "1";
                }
                return null;
            }
            string str = this.TaskRule.Split(new char[] { ' ' }).GetValue((int)rulePart).ToString();
            switch (str)
            {
                case "*":
                case "?":
                    if (RulePart.DayOfWeek != rulePart)
                    {
                        return "1";
                    }
                    return null;
            }
            if (str.Contains("/"))
            {
                return str.Substring(str.IndexOf("/") + 1);
            }
            return str;
        }
        /// <summary>
        /// 扩展属性
        /// </summary>
        [Ignore]
        public NameValueCollection ExtendedProperties
        {
            get { return this.PropertySerializer.Properties; }
            set { this.PropertySerializer.SetNameValueCollection(value); }
        }
        ///<summary>
        ///实例化实体
        ///</summary>
        ///<remarks>实例化实体时先根据taskName从数据库中获取，如果取不到则创建新实例</remarks>
        public static TaskDetailEntity New()
        {
            return new TaskDetailEntity();
        }

        public string ClassType { get; set; }

        public bool Enabled { get; set; }

        public DateTime? EndDate { get; set; }

        public int Id { get; set; }

        public bool IsRunning { get; set; }

        public DateTime? LastEnd { get; set; }

        public bool? LastIsSuccess { get; set; }

        public DateTime? LastStart { get; set; }

        public string Name { get; set; }

        public DateTime? NextStart { get; set; }

        public bool RunAtRestart { get; set; }

        public RunAtServer RunAtServer { get; set; }

        public DateTime StartDate { get; set; }

        public string TaskRule { get; set; }
    }
}