using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Task
{
    public interface ITaskExecutionContext
    {
        object Get(object key);
        void Put(object key, object objectValue);
    }
}
