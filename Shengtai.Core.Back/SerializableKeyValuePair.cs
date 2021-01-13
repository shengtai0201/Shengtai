using System;
using System.Collections.Generic;

namespace Shengtai
{
    [Serializable]
    public class SerializableKeyValuePair<TKey, TValue>
    {
        [NonSerialized]
        private KeyValuePair<TKey, TValue> keyValuePair;

        public TKey Key
        {
            get
            {
                return this.keyValuePair.Key;
            }
            set
            {
                TValue v = this.keyValuePair.Value;
                this.keyValuePair = new KeyValuePair<TKey, TValue>(value, v);
            }
        }

        public TValue Value
        {
            get
            {
                return this.keyValuePair.Value;
            }
            set
            {
                TKey k = this.keyValuePair.Key;
                this.keyValuePair = new KeyValuePair<TKey, TValue>(k, value);
            }
        }

        public SerializableKeyValuePair()
        {
            this.keyValuePair = new KeyValuePair<TKey, TValue>();
        }

        public SerializableKeyValuePair(TKey key, TValue value)
        {
            this.keyValuePair = new KeyValuePair<TKey, TValue>(key, value);
        }

        public SerializableKeyValuePair(KeyValuePair<TKey, TValue> pair)
        {
            this.keyValuePair = pair;
        }
    }
}
