namespace NClicker.Storage
{
    public interface IStorage<in TKey, TValue>
    {
        void Store(TKey key, TValue value);

        void Remove(TKey key);

        TValue Acquire(TKey key);

        void Clear();

        int Count { get; }

        TValue this[TKey key]
        {
            get; set;
        }
    }
}