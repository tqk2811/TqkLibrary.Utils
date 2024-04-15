using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TqkLibrary.Utils
{
    /// <summary>
    /// 
    /// </summary>
    public static class DeepCopyHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="jsonSerializerSettings"></param>
        /// <returns></returns>
        public static T CloneByJson<T>(this T obj, JsonSerializerSettings? jsonSerializerSettings = null)
        {
            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(obj, jsonSerializerSettings), jsonSerializerSettings)!;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="type"></param>
        /// <param name="jsonSerializerSettings"></param>
        /// <returns></returns>
        public static object? CloneByJson(this object obj, Type type, JsonSerializerSettings? jsonSerializerSettings = null)
        {
            return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(obj, jsonSerializerSettings), type, jsonSerializerSettings)!;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="current"></param>
        /// <param name="target"></param>
        /// <param name="jsonSerializerSettings"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void DeepCopyTo<T>(this T current, T? target, JsonSerializerSettings? jsonSerializerSettings = null) where T : class
        {
            if (current is null) throw new ArgumentNullException(nameof(current));
            if (target is null) throw new ArgumentNullException(nameof(target));
            current.DeepCopyTo(target, current.GetType(), jsonSerializerSettings);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="current"></param>
        /// <param name="target"></param>
        /// <param name="type"></param>
        /// <param name="jsonSerializerSettings"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="NotSupportedException"></exception>
        public static void DeepCopyTo(this object current, object target, Type type, JsonSerializerSettings? jsonSerializerSettings = null)
        {
            if (current is null) throw new ArgumentNullException(nameof(current));
            if (target is null) throw new ArgumentNullException(nameof(target));
            if (type is null) throw new ArgumentNullException(nameof(type));
            if (type.IsValueType || type.IsArray)
                throw new InvalidOperationException($"Type '{type.FullName}' can't be enum, struct, array");
            //check type first

            void CloneIList(IList current, IList target, JsonSerializerSettings? jsonSerializerSettings = null)
            {
                target.Clear();
                foreach (var item in current)
                {
                    target.Add(item.CloneByJson(item.GetType(), jsonSerializerSettings));
                }
            }
            void CloneIDictionary(IDictionary current, IDictionary target, JsonSerializerSettings? jsonSerializerSettings = null)
            {
                target.Clear();
                foreach (DictionaryEntry pair in (IDictionary)current.CloneByJson(current.GetType(), jsonSerializerSettings)!)
                {
                    target[pair.Key] = pair.Value;
                }
            }

            if (current is IEnumerable)
            {
                if (current is IList list_current && target is IList list_target)
                {
                    CloneIList(list_current, list_target, jsonSerializerSettings);
                }
                else if (current is IDictionary dict_current && target is IDictionary dict_target)
                {
                    CloneIDictionary(dict_current, dict_target, jsonSerializerSettings);
                }
                else throw new NotSupportedException(type.FullName);
            }
            else
            {
                foreach (PropertyInfo propertyInfo in type.GetProperties().Where(x => x.CanWrite && x.CanRead))
                {
                    object? f_current = propertyInfo.GetValue(current);
                    if (f_current is null)
                    {
                        propertyInfo.SetValue(target, null);
                    }
                    else
                    {
                        if (propertyInfo.PropertyType.IsValueType ||
                            propertyInfo.PropertyType.IsEnum ||
                            propertyInfo.PropertyType.Equals(typeof(string))
                            )//struct/enum or string
                        {
                            propertyInfo.SetValue(target, propertyInfo.GetValue(current));
                        }
                        else if (propertyInfo.PropertyType.IsArray)//array
                        {
                            propertyInfo.SetValue(target, f_current.CloneByJson(f_current.GetType(), jsonSerializerSettings));
                        }
                        else if (typeof(IEnumerable).IsAssignableFrom(propertyInfo.PropertyType))
                        {
                            object? f_target = propertyInfo.GetValue(target);
                            if (f_target is null)
                            {
                                //just clone
                                propertyInfo.SetValue(target, f_current.CloneByJson(f_current.GetType(), jsonSerializerSettings));
                            }
                            else
                            {
                                if (f_target is IList list_target)//just dont break pointer of List<T>
                                {
                                    CloneIList((IList)f_current, list_target, jsonSerializerSettings);
                                }
                                else if (f_target is IDictionary dict_target)//just dont break pointer of Dictionary<TKey,TValue>
                                {
                                    CloneIDictionary((IDictionary)f_current, dict_target, jsonSerializerSettings);
                                }
                                else throw new NotSupportedException(propertyInfo.PropertyType.FullName);
                            }
                        }
                        else//class
                        {
                            var f_target = propertyInfo.GetValue(target);
                            if (f_target is null)
                            {
                                propertyInfo.SetValue(target, f_current.CloneByJson(f_current.GetType(), jsonSerializerSettings));
                            }
                            else
                            {
                                f_current.DeepCopyTo(f_target, f_current.GetType());
                            }
                        }
                    }
                }
            }
        }
    }
}
