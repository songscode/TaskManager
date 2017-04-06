using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Task
{
    public interface ITaskExecutionContext
    {
        ///<summary>
        ///获取propertyName指定的属性值
        ///</summary>
        ///<remarks>
        ///不能显式实现
        ///</remarks>
        ///<param name="propertyName">序列化属性名称字符串</param>
        T GetExtendedProperty<T>(string propertyName);
        T GetExtendedProperty<T>(string propertyName, T defaultValue);
        ///<summary>
        ///把扩展属性序列化
        ///</summary>
        ///<remarks>
        ///需要显式实现
        ///</remarks>
        void Serialize();
        ///<summary>
        ///设置扩展属性
        ///</summary>
        ///<remarks>
        ///不能显式实现
        ///</remarks>
        ///<param name="propertyName">属性名称</param>
        ///<param name="propertyValue">属性值</param>
        void SetExtendedProperty(string propertyName, object propertyValue);
    }
}
