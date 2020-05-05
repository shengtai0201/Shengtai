using System;

namespace Shengtai.Core
{
    public static partial class SystemExtensions
    {
        public static SerializableKeyValuePair<int, string> GetEnumKeyValue(this Enum e, bool emptyValue = false)
        {
            int key = Convert.ToInt32(e);
            string value = emptyValue ? string.Empty : e.GetEnumDescription();

            return new SerializableKeyValuePair<int, string>(key, value);
        }
    }
}
