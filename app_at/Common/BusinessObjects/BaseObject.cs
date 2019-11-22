using Common.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;

namespace Common.BusinessObjects
{
    [DataContract]
    public class BaseObject
    {
        /// <summary>
        /// Compares the properties of two objects of the same type and returns if all properties are equal.
        /// </summary>
        /// <param name="objectB">The second object to compare.</param>
        /// <returns><c>true</c> if all property values are equal, otherwise <c>false</c>.</returns>
        public override bool Equals(object objectB)
        {
            Logger.Debug("Compare this: " + this);
            Logger.Debug("with object: " + objectB);

            bool result;

            if (this != null && objectB != null)
            {
                Type objectType = this.GetType();

                result = true; // assume by default they are equal

                foreach (PropertyInfo propertyInfo in objectType.GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => p.CanRead))
                {
                    var valueA = propertyInfo.GetValue(this, null);
                    var valueB = propertyInfo.GetValue(objectB, null);

                    // if it is a primitive type, value type or implements
                    // IComparable, just directly try and compare the value
                    if (CanDirectlyCompare(propertyInfo.PropertyType))
                    {
                        if (!AreValuesEqual(valueA, valueB))
                        {
                            Logger.Debug($"Mismatch with property '{objectType.FullName}.{propertyInfo.Name}' found");
                            Logger.Debug($"Expected: '{valueA}', But found: '{valueB}'");
                            result = false;
                        }
                    }
                    // if it implements IEnumerable, then scan any items
                    else if (typeof(IEnumerable).IsAssignableFrom(propertyInfo.PropertyType))
                    {
                        IEnumerable<object> collectionItems1;
                        IEnumerable<object> collectionItems2;
                        int collectionItemsCount1;
                        int collectionItemsCount2;

                        // null check
                        if (valueA == null && valueB != null || valueA != null && valueB == null)
                        {
                            Logger.Debug($"Mismatch with property '{objectType.FullName}.{propertyInfo.Name}' found");
                            Logger.Debug($"Expected: '{valueA}', But found: '{valueB}'");
                            result = false;
                        }
                        else if (valueA != null && valueB != null)
                        {
                            collectionItems1 = ((IEnumerable)valueA).Cast<object>();
                            collectionItems2 = ((IEnumerable)valueB).Cast<object>();
                            collectionItemsCount1 = collectionItems1.Count();
                            collectionItemsCount2 = collectionItems2.Count();

                            // check the counts to ensure they match
                            if (collectionItemsCount1 != collectionItemsCount2)
                            {
                                Logger.Debug($"Collection counts for property '{objectType.FullName}.{propertyInfo.Name}' do not match");
                                Logger.Debug($"Expected: '{collectionItemsCount1}', But found: '{collectionItemsCount2}'");
                                result = false;
                            }
                            // and if they do, compare each item...
                            // this assumes both collections have the same order
                            else
                            {
                                for (int i = 0; i < collectionItemsCount1; i++)
                                {
                                    object collectionItem1;
                                    object collectionItem2;
                                    Type collectionItemType;

                                    collectionItem1 = collectionItems1.ElementAt(i);
                                    collectionItem2 = collectionItems2.ElementAt(i);
                                    collectionItemType = collectionItem1.GetType();

                                    if (CanDirectlyCompare(collectionItemType))
                                    {
                                        if (!AreValuesEqual(collectionItem1, collectionItem2))
                                        {
                                            Logger.Debug($"Item {i} in property collection '{objectType.FullName}.{propertyInfo.Name}' does not match");
                                            Logger.Debug($"Expected: '{collectionItem1}', But found: '{collectionItem2}'");
                                            result = false;
                                        }
                                    }
                                    else if (!Equals(collectionItem1, collectionItem2))
                                    {
                                        Logger.Debug($"Item {i} in property collection '{objectType.FullName}.{propertyInfo.Name}' does not match");
                                        Logger.Debug($"Expected: '{collectionItem1}', But found: '{collectionItem2}'");
                                        result = false;
                                    }
                                }
                            }
                        }
                    }
                    else if (propertyInfo.PropertyType.IsClass)
                    {
                        if (!Equals(propertyInfo.GetValue(this, null), propertyInfo.GetValue(objectB, null)))
                        {
                            Logger.Debug($"Mismatch with property '{objectType.FullName}.{propertyInfo.Name}' found");
                            result = false;
                        }
                    }
                    else
                    {
                        Logger.Debug($"Cannot compare property '{objectType.FullName}.{propertyInfo.Name}'");
                        result = false;
                    }
                }
            }
            else
            {
                result = Equals(this, objectB);
            }

            return result;
        }

        /// <summary>
        /// Determines whether value instances of the specified type can be directly compared.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>
        ///     <c>true</c> if this value instances of the specified type can be directly compared; otherwise, <c>false</c>.
        /// </returns>
        private bool CanDirectlyCompare(Type type)
        {
            return typeof(IComparable).IsAssignableFrom(type) || type.IsPrimitive || type.IsValueType;
        }

        /// <summary>
        /// Compares two values and returns if they are the same.
        /// </summary>
        /// <param name="valueA">The first value to compare.</param>
        /// <param name="valueB">The second value to compare.</param>
        /// <returns><c>true</c> if both values match, otherwise <c>false</c>.</returns>
        private bool AreValuesEqual(object valueA, object valueB)
        {
            bool result;

            IComparable selfValueComparer = valueA as IComparable;

            if (valueA == null && valueB != null || valueA != null && valueB == null)
            {
                result = false; // one of the values is null
            }
            else if (selfValueComparer != null && selfValueComparer.CompareTo(valueB) != 0)
            {
                result = false; // the comparison using IComparable failed
            }
            else if (!object.Equals(valueA, valueB))
            {
                result = false; // the comparison using Equals failed
            }
            else
            {
                result = true; // match
            }

            return result;
        }

        public override int GetHashCode()
        {
            PropertyInfo[] propertyInfos = this.GetType().GetProperties();

            int hashCode = 555718604;

            foreach (var info in propertyInfos)
            {
                var value = info.GetValue(this, null) ?? "(null)";
                hashCode = hashCode * -1521134295 + EqualityComparer<object>.Default.GetHashCode(value);
            }

            return hashCode;
        }

        /// <summary>
        /// Override basic ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            PropertyInfo[] propertyInfos = this.GetType().GetProperties();

            StringBuilder sb = new StringBuilder();

            foreach (var info in propertyInfos)
            {
                if (info.PropertyType.IsSubclassOf(typeof(BaseObject)))
                {
                    sb.AppendLine(info.Name + ": [");
                    sb.AppendLine(info.ToString());
                    sb.AppendLine("]");
                }
                if (info.PropertyType != typeof(string) && typeof(IEnumerable).IsAssignableFrom(info.PropertyType))
                {
                    sb.AppendLine(info.Name + ": [");
                    IEnumerable coll = (IEnumerable)info.GetValue(this, null);
                    if (coll == null)
                    {
                        sb.AppendLine("null");
                        sb.AppendLine("]");
                        continue;
                    }
                    foreach (object item in coll)
                    {
                        sb.AppendLine(item.ToString());
                    }
                    sb.AppendLine("]");
                }
                else
                {
                    object value = info.GetValue(this, null) ?? "(null)";
                    sb.AppendLine(info.Name + ": " + value.ToString());
                }
            }

            return MethodBase.GetCurrentMethod().DeclaringType.ToString() + "[\n" + sb.ToString() + "]";
        }
    }
}
