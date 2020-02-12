using Newtonsoft.Json;
using System;
using System.Runtime.InteropServices;

namespace NClicker.Storage
{
    public class StorageItem<TKey, TValue>
    {
        public string Identifier { get; set; }
        public TKey Key { get; set; }
        public TValue Value { get; set; }

        public StorageItem(TKey key, TValue value)
        {
            Identifier = Constants.IsNetCore ? JsonConvert.SerializeObject(key) : key.GetHashCode().ToString();
            Key = Key;
            Value = value;
        }

        public StorageItem()
        {
        }
    }
}