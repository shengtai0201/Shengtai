using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Shengtai
{
    public static partial class Extensions
    {
        public static string InnerException(this Exception e, string methodName, StringBuilder messageBuilder = null)
        {
            if (messageBuilder == null)
                messageBuilder = new StringBuilder();

            messageBuilder.AppendLine(e.Message);

            if (e.InnerException != null)
                return InnerException(e.InnerException, methodName, messageBuilder);
            else
                return $"{methodName}: {messageBuilder.ToString()}";
        }

        public static string InnerException(this DbException e, string methodName, StringBuilder messageBuilder = null)
        {
            if (messageBuilder == null)
                messageBuilder = new StringBuilder();

            messageBuilder.AppendLine($"Message: {e.Message}");
            messageBuilder.AppendLine($"Detail: {e.StackTrace}");

            if (e.InnerException != null)
                return InnerException(e.InnerException, methodName, messageBuilder);
            else
                return $"{methodName}: {messageBuilder.ToString()}";
        }

        public static string ToUnixTimeStamp(this DateTime dateTime)
        {
            return ((int)dateTime.Subtract(new DateTime(1970, 1, 1)).TotalSeconds).ToString();
        }

        public static string GetEnumDescription(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), inherit: false);

            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }

        //public static KeyValuePair<int, string> GetEnumKeyValuePair(this Enum value)
        //{
        //    int key = Convert.ToInt32(value);
        //    return new KeyValuePair<int, string>(key, value.GetEnumDescription());
        //}

        public static KeyValuePair<int, string> GetEnumKeyValue(this Enum e, bool emptyValue = false)
        {
            int key = Convert.ToInt32(e);
            string value = emptyValue ? string.Empty : e.GetEnumDescription();

            return new KeyValuePair<int, string>(key, value);
        }

        public static TEnum GetEnum<TEnum>(this KeyValuePair<int, string> value) where TEnum : struct
        {
            if (Enum.TryParse(value.Key.ToString(), out TEnum e))
                return e;

            return default(TEnum);
        }

        public static string GetEnumDescription<TEnum>(this string value) where TEnum : struct
        {
            if (Enum.TryParse(value, out TEnum result))
                return GetEnumDescription(result as Enum);

            return null;
        }

        public static string GetEnumDescription<TEnum>(this int value) where TEnum : struct
        {
            return GetEnumDescription<TEnum>(value.ToString());
        }

        public static ICollection<KeyValuePair<int, string>> GetEnumDictionary<TEnum>(params Enum[] skips) where TEnum : struct
        {
            ICollection<KeyValuePair<int, string>> keyValues = new List<KeyValuePair<int, string>>();

            var values = Enum.GetValues(typeof(TEnum));
            foreach (int key in values)
            {
                if (Enum.TryParse(key.ToString(), out TEnum result))
                {
                    var current = result as Enum;
                    if (skips.Contains(current))
                        continue;
                }

                var value = key.GetEnumDescription<TEnum>();
                keyValues.Add(new KeyValuePair<int, string>(key, value));
            }

            return keyValues;
        }

        public static T RunSync<T>(Func<Task<T>> item)
        {
            var oldContext = SynchronizationContext.Current;

            var syncContext = new ExclusiveSynchronizationContext();
            SynchronizationContext.SetSynchronizationContext(syncContext);
            T result = default;
            syncContext.Post(async _ =>
            {
                try
                {
                    result = await item();
                }
                finally
                {
                    syncContext.EndMessageLoop();
                }
            }, null);
            syncContext.BeginMessageLoop();

            SynchronizationContext.SetSynchronizationContext(oldContext);
            return result;
        }

        public static string DbToString(object value)
        {
            if (value == DBNull.Value)
                return null;

            return value.ToString();
        }
    }
}