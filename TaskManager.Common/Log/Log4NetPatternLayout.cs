using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net.Layout;

namespace TaskManager.Common.Log
{
    class Log4NetPatternLayout : PatternLayout
    {
        public Log4NetPatternLayout()
        {
            this.AddConverter("property", typeof(Log4NetPatternLayoutConverter));
        }
    }
}
