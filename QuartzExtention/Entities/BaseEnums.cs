namespace TaskManager.Task.Entities
{
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
