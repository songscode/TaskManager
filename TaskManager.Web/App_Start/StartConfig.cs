using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac;
using Microsoft.Ajax.Utilities;
using TaskManager.Task;
using TaskManager.Task.Quartz;

namespace TaskManager.Web.App_Start
{
    public class StartConfig
    {
        public static void Start()
        {
            InitializeDIContainer();
            InitializeApplication();
        }

        /// <summary>
        /// 初始化DI容器
        /// </summary>
        private static void InitializeDIContainer()
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.Register(c => new QuartzTaskScheduler()).As<ITaskScheduler>().SingleInstance();
        }

        /// <summary>
        /// 初始化应用程序，加载基础数据
        /// </summary>
        private static void InitializeApplication()
        {
            //启动定时任务
            TaskSchedulerFactory.GetScheduler().Start();
        }
    }
}