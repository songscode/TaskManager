using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuartzExtention
{
    public class TaskDetail
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

        ///<summary>
        ///实例化实体
        ///</summary>
        ///<remarks>实例化实体时先根据taskName从数据库中获取，如果取不到则创建新实例</remarks>
        public static TaskDetail New()
        {
            return new TaskDetail();
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

    public enum RulePart
    {
        ///<summary>
        ///秒域
        ///</summary>
        Seconds,
        ///<summary>
        ///分钟域
        ///</summary>
        Minutes,
        ///<summary>
        ///小时域
        ///</summary>
        Hours,
        ///<summary>
        ///日期域
        ///</summary>
        Day,
        ///<summary>
        ///规则月部分 
        ///</summary>
        Mouth,
        ///<summary>
        ///规则星期域
        ///</summary>
        DayOfWeek
    }

    ///<summary>
    ///任务在哪台服务器上运行
    ///</summary>
    public enum RunAtServer
    {
        ///<summary>
        ///分布式环境下的集群服务器主控端
        ///</summary>
        Master,
        ///<summary>
        ///分布式环境下的集群服务器计算节点
        ///</summary>
        Slave
    }
}
