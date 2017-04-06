using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Text;

namespace Common.Task
{
    ///<summary>
    ///属性序列化器
    ///</summary>
    public class PropertySerializer
    {
        private NameValueCollection _nameValueCollection = new NameValueCollection();

        public PropertySerializer(string propertyNames, string propertyValues)
        {
            if (!string.IsNullOrEmpty(propertyNames) && !string.IsNullOrEmpty(propertyValues))
            {
                this._nameValueCollection = SerializeHandle(propertyNames, propertyValues);
            }
            else
            {
                this._nameValueCollection = new NameValueCollection();
            }
        }
        /// <summary>
        ///propertyNames，propertyValues序列化为NameValueCollection
        /// </summary>
        /// <param name="propertyNames"></param>
        /// <param name="propertyValues"></param>
        /// <returns></returns>
        private static NameValueCollection SerializeHandle(string propertyNames, string propertyValues)
        {
            NameValueCollection values = new NameValueCollection();
            if (((propertyNames != null) && (propertyValues != null)) && ((propertyNames.Length > 0) && (propertyValues.Length > 0)))
            {
                char[] separator = new char[] { ':' };
                string[] strArray = propertyNames.Split(separator);
                for (int i = 0; i < (strArray.Length / 4); i++)
                {
                    int startIndex = int.Parse(strArray[(i * 4) + 2], CultureInfo.InvariantCulture);
                    int length = int.Parse(strArray[(i * 4) + 3], CultureInfo.InvariantCulture);
                    string str = strArray[i * 4];
                    if (((strArray[(i * 4) + 1] == "S") && (startIndex >= 0)) && ((length > 0) && (propertyValues.Length >= (startIndex + length))))
                    {
                        values[str] = propertyValues.Substring(startIndex, length);
                    }
                }
            }
            return values;
        }
        /// <summary>
        /// NameValueCollection序列化为propertyNames，propertyValues
        /// </summary>
        /// <param name="nameValueCollection"></param>
        /// <param name="propertyNames"></param>
        /// <param name="propertyValues"></param>
        private static void SerializeHandle(NameValueCollection nameValueCollection, ref string propertyNames, ref string propertyValues)
        {
            if ((nameValueCollection != null) && (nameValueCollection.Count != 0))
            {
                StringBuilder builder = new StringBuilder();
                StringBuilder builder2 = new StringBuilder();
                int num2 = 0;
                foreach (string str in nameValueCollection.AllKeys)
                {
                    if (str.IndexOf(':') != -1)
                    {
                        throw new ArgumentException("SerializableProperties Name can not contain the character \":\"");
                    }
                    string str2 = nameValueCollection[str];
                    if (!string.IsNullOrEmpty(str2))
                    {
                        builder.AppendFormat("{0}:S:{1}:{2}:", str, num2, str2.Length);
                        builder2.Append(str2);
                        num2 += str2.Length;
                    }
                }
                propertyNames = builder.ToString();
                propertyValues = builder2.ToString();
            }
        }

        ///<summary>
        ///获取propertyName指定的属性值
        ///</summary>
        ///<param name="propertyName">属性名称</param>
        public T GetExtendedProperty<T>(string propertyName)
        {
            if (typeof(T) == typeof(string))
            {
                TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));
                return this.GetExtendedProperty<T>(propertyName, (T) converter.ConvertFrom(string.Empty));
            }
            return this.GetExtendedProperty<T>(propertyName, default(T));
        }
        /// <summary>
        ///获取propertyName指定的属性值,若没有，默认defaultValue
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyName"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public T GetExtendedProperty<T>(string propertyName, T defaultValue)
        {
            string str = this._nameValueCollection[propertyName];
            if (str == null)
            {
                return defaultValue;
            }
            return (T) Convert.ChangeType(str, typeof(T));
        }
        /// <summary>
        /// 序列化为propertyNames，propertyValues
        /// </summary>
        /// <param name="propertyNames"></param>
        /// <param name="propertyValues"></param>
        public void Serialize(ref string propertyNames, ref string propertyValues)
        {
            SerializeHandle(this._nameValueCollection, ref propertyNames, ref propertyValues);
        }
        /// <summary>
        /// 加入集合
        /// </summary>
        /// <param name="nameValueCollection"></param>
        public void SetNameValueCollection(NameValueCollection nameValueCollection)
        {
            if (nameValueCollection == null) throw new ArgumentNullException(nameof(nameValueCollection));
            this._nameValueCollection = nameValueCollection;
        }
        /// <summary>
        /// 属性集合
        /// </summary>
        public NameValueCollection Properties { get { return this._nameValueCollection; } }

        ///<summary>
        ///设置扩展属性
        ///</summary>
        ///<param name="propertyName">属性名称</param>
        ///<param name="propertyValue">属性值</param>
        public void SetExtendedProperty(string propertyName, object propertyValue)
        {
            if (propertyValue == null)
            {
                this._nameValueCollection.Remove(propertyName);
                return;
            }
            string str = propertyValue.ToString().Trim();
            if (string.IsNullOrEmpty(str))
            {
                this._nameValueCollection.Remove(propertyName);
            }
            else
            {
                this._nameValueCollection[propertyName] = str;
            }
        }
    }
}

