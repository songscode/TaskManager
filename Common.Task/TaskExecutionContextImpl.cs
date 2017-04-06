using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Task
{
    public class TaskExecutionContextImpl: ITaskExecutionContext
    {
        private PropertySerializer _propertySerializer;
        private string _propertyNames;
        private string _propertyValues;

        protected TaskExecutionContextImpl()
        {

        }

        public void Serialize()
        {
            this.PropertySerializer.Serialize(ref this._propertyNames, ref this._propertyValues);
        }

        ///<summary>
        ///获取propertyName指定的属性值
        ///</summary>
        ///<param name="propertyName">属性名称</param>
        public T GetExtendedProperty<T>(string propertyName)
        {
            return this.PropertySerializer.GetExtendedProperty<T>(propertyName);
        }

        public T GetExtendedProperty<T>(string propertyName, T defaultValue)
        {
            return this.PropertySerializer.GetExtendedProperty<T>(propertyName, defaultValue);
        }

        ///<summary>
        ///设置可序列化属性
        ///</summary>
        ///<param name="propertyName">属性名称</param>
        ///<param name="propertyValue">属性值</param>
        public void SetExtendedProperty(string propertyName, object propertyValue)
        {
            this.PropertySerializer.SetExtendedProperty(propertyName, propertyValue);
        }

        ///<summary>
        ///序列化属性名称字符串
        ///</summary>
        ///<remarks>
        ///保留该属性的目的是通过orm存取数据库的数据
        ///</remarks>
        public string PropertyNames
        {
            get
            {
                return this._propertyNames;
            }
            protected set
            {
                this._propertyNames = value;
            }
        }

        protected PropertySerializer PropertySerializer
        {
            get
            {
                if (this._propertySerializer == null)
                {
                    this._propertySerializer = new PropertySerializer(this.PropertyNames, this.PropertyValues);
                }
                return this._propertySerializer;
            }
        }

        ///<summary>
        ///序列化属性值字符串
        ///</summary>
        ///<remarks>
        ///保留该属性的目的是通过orm存取数据库的数据
        ///</remarks>
        public string PropertyValues
        {
            get
            {
                return this._propertyValues;
            }
            protected set
            {
                this._propertyValues = value;
            }
        }

    }
}
