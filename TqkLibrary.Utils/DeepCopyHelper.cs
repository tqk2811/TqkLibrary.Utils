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
    public enum ListCloneHandle
    {
        /// <summary>
        /// Add
        /// </summary>
        Add,
        /// <summary>
        /// Clear target and add all current to target
        /// </summary>
        ClearTargetAndAdd,
        /// <summary>
        /// Check via Equal
        /// </summary>
        AddNoDuplicates
    }
    /// <summary>
    /// 
    /// </summary>
    public enum DictionaryCloneHandle
    {
        /// <summary>
        /// Replace value for IDictionary 
        /// </summary>
        Default,
        /// <summary>
        /// Clear target and add all current to target
        /// </summary>
        ClearTarget,
        /// <summary>
        /// Not replace value if target key exist
        /// </summary>
        NotReplaceValue
    }
    /// <summary>
    /// 
    /// </summary>
    public class DeepCopySetting
    {
        /// <summary>
        /// 
        /// </summary>
        public JsonSerializerSettings? JsonSerializerSettings { get; set; }

        /// <summary>
        /// Does not work with IEnumerable and its children
        /// </summary>
        public bool IsAllowCopyNullToTarget { get; set; } = true;

        /// <summary>
        /// 
        /// </summary>
        public ListCloneHandle ListCollectionCloneHandle { get; set; } = ListCloneHandle.Add;
        /// <summary>
        /// 
        /// </summary>
        public DictionaryCloneHandle DictionaryCloneHandle { get; set; } = DictionaryCloneHandle.Default;
    }
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
        public static T CloneByJson<T>(this object obj, JsonSerializerSettings? jsonSerializerSettings = null)
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
        /// <exception cref="ArgumentNullException"></exception>
        public static void DeepCopyTo<T>(this T current, T? target, DeepCopySetting? deepCopySetting = null) where T : class
        {
            if (current is null) throw new ArgumentNullException(nameof(current));
            if (target is null) throw new ArgumentNullException(nameof(target));
            current.DeepCopyTo(target, current.GetType(), deepCopySetting);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="NotSupportedException"></exception>
        public static void DeepCopyTo(this object current, object target, Type type, DeepCopySetting? deepCopySetting = null)
        {
            if (current is null) throw new ArgumentNullException(nameof(current));
            if (target is null) throw new ArgumentNullException(nameof(target));
            if (type is null) throw new ArgumentNullException(nameof(type));
            if (type.IsValueType || type.IsArray)
                throw new InvalidOperationException($"Type '{type.FullName}' can't be enum, struct, array");
            if (deepCopySetting is null) 
                deepCopySetting = new();
            //check type first

            void CloneIList(IList current, IList target, DeepCopySetting _deepCopySetting)
            {
                if(_deepCopySetting.ListCollectionCloneHandle == ListCloneHandle.ClearTargetAndAdd)
                    target.Clear();
                foreach (var item in current)
                {
                    switch(_deepCopySetting.ListCollectionCloneHandle)
                    {
                        case ListCloneHandle.Add:
                        case ListCloneHandle.ClearTargetAndAdd:
                            target.Add(item.CloneByJson(item.GetType(), _deepCopySetting.JsonSerializerSettings));
                            break;

                        case ListCloneHandle.AddNoDuplicates:
                            if(!target.Contains(item))
                                target.Add(item.CloneByJson(item.GetType(), _deepCopySetting.JsonSerializerSettings));
                            break;

                        default: throw new NotSupportedException(_deepCopySetting.ListCollectionCloneHandle.ToString());
                    }
                }
            }
            void CloneIDictionary(IDictionary current, IDictionary target, DeepCopySetting _deepCopySetting)
            {
                if (_deepCopySetting.DictionaryCloneHandle == DictionaryCloneHandle.ClearTarget)
                    target.Clear();
                foreach (DictionaryEntry pair in (IDictionary)current.CloneByJson(current.GetType(), _deepCopySetting.JsonSerializerSettings)!)
                {
                    switch(_deepCopySetting.DictionaryCloneHandle)
                    {
                        case DictionaryCloneHandle.ClearTarget:
                        case DictionaryCloneHandle.Default:
                            target[pair.Key] = pair.Value;
                            break;

                        case DictionaryCloneHandle.NotReplaceValue:
                            if(!target.Contains(pair.Key))
                                target[pair.Key] = pair.Value;
                            break;

                        default: throw new NotSupportedException(_deepCopySetting.DictionaryCloneHandle.ToString());
                    }
                }
            }

            if (current is IEnumerable)
            {
                if (current is IList list_current && target is IList list_target)
                {
                    CloneIList(list_current, list_target, deepCopySetting);
                }
                else if (current is IDictionary dict_current && target is IDictionary dict_target)
                {
                    CloneIDictionary(dict_current, dict_target, deepCopySetting);
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
                        if (deepCopySetting.IsAllowCopyNullToTarget)
                        {
                            propertyInfo.SetValue(target, null);
                        }
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
                            propertyInfo.SetValue(target, f_current.CloneByJson(f_current.GetType(), deepCopySetting.JsonSerializerSettings));
                        }
                        else if (typeof(IEnumerable).IsAssignableFrom(propertyInfo.PropertyType))
                        {
                            object? f_target = propertyInfo.GetValue(target);
                            if (f_target is null)
                            {
                                //just clone
                                propertyInfo.SetValue(target, f_current.CloneByJson(f_current.GetType(), deepCopySetting.JsonSerializerSettings));
                            }
                            else
                            {
                                if (f_target is IList list_target)//just dont break pointer of List<T>
                                {
                                    CloneIList((IList)f_current, list_target, deepCopySetting);
                                }
                                else if (f_target is IDictionary dict_target)//just dont break pointer of Dictionary<TKey,TValue>
                                {
                                    CloneIDictionary((IDictionary)f_current, dict_target, deepCopySetting);
                                }
                                else throw new NotSupportedException(propertyInfo.PropertyType.FullName);
                            }
                        }
                        else//class
                        {
                            var f_target = propertyInfo.GetValue(target);
                            if (f_target is null)
                            {
                                propertyInfo.SetValue(target, f_current.CloneByJson(f_current.GetType(), deepCopySetting.JsonSerializerSettings));
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
