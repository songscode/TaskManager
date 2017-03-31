using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Task
{
    public class TaskExecutionContextImpl: ITaskExecutionContext
    {
        private readonly IDictionary<object, object> _data = new Dictionary<object, object>();

        /// <summary>
        /// 设定key对应的参数
        /// </summary>
        /// <param name="key"></param>
        /// <param name="objectValue"></param>
        public virtual void Put(object key, object objectValue)
        {
            _data[key] = objectValue;
        }
        /// <summary>
        /// 读取key对应的参数
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual object Get(object key)
        {
            object retValue;
            _data.TryGetValue(key, out retValue);
            return retValue;
        }
    }
}
