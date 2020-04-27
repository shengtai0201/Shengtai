using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Shengtai
{
    [Serializable]
    [DataContract]
    public class SerializableKeyValuePair<TKey, TValue>
    {
        [IgnoreDataMember]
        private KeyValuePair<TKey, TValue> keyValuePair;

        [DataMember]
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

        [DataMember]
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