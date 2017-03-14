using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;

namespace TaskManager.Common.Log
{
    public class LogHelper<T> where T : class
    {
        public static ILog Logger { get { return LogManager.GetLogger(typeof(T)); } }

        public static void Trace(T message)
        {
            Logger.Trace(message);
        }

        public static void Trace(T message, Exception exception)
        {
            Logger.Trace(message, exception);
        }

        public static void Debug(T message)
        {
            Logger.Debug(message);
        }

        public static void Debug(T message, Exception exception)
        {
            Logger.Debug(message, exception);
        }
        
        public static void Info(T message)
        {
            Logger.Info(message);
        }

        public static void Info(T message, Exception exception)
        {
            Logger.Info(message, exception);
        }

        public static void Warn(T message)
        {
            Logger.Warn(message);
        }

        public static void Warn(T message, Exception exception)
        {
            Logger.Warn(message, exception);
        }
        
        public static void Error(T message)
        {
            Logger.Error(message);
        }

        public static void Error(T message, Exception exception)
        {
            Logger.Error(message, exception);
        }
        

        public static void Fatal(T message)
        {
            Logger.Fatal(message);
        }

        public static void Fatal(T message, Exception exception)
        {
            Logger.Fatal(message, exception);
        }
        
    }

    public class LogHelper
    {
        public static ILog SystemLog { get { return LogManager.GetLogger("SystemLog"); } }
    }
}
