﻿using LiteDB;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace NClicker.Storage
{
    public class LiteDatabaseMapper<TKey, TValue> : IStorage<TKey, TValue>
    {
        /// <summary>
        /// In .NET Core the hashcodes of objects are not immutable/consistent,
        /// we must have strategy for handling of the keys for the .NET Core.
        /// </summary>
       

        private const string StorageName = @"IStorage.db";

        private readonly LiteCollection<BsonDocument> _collection;
        private readonly LiteDatabase _database;

        private readonly string _collectionName;

        public int Count { get; private set; }

        public LiteDatabaseMapper(string collection = "Main")
        {
            _database = new LiteDatabase(StorageName);
            _collectionName = collection;
            _collection = _database.GetCollection(_collectionName);
            Count = _database.GetCollection(_collectionName).Count();
        }

        public TValue this[TKey key]
        {
            get => Acquire(key);
            set => Store(key, value);
        }

        public TValue Acquire(TKey key)
        {
            var mapper = BsonMapper.Global;
            var result = _collection.FindOne((x) => x["Identifier"].AsString.Equals(
                Constants.IsNetCore ? JsonConvert.SerializeObject(key) : key.GetHashCode().ToString()));
            return result != null ? mapper.ToObject<StorageItem<TKey, TValue>>(result).Value : default;
        }

        public void Store(TKey key, TValue value)
        {
            var mappedDocument = BsonMapper.Global.ToDocument(new StorageItem<TKey, TValue>(key, value));

            _collection.Upsert(mappedDocument["Identifier"], mappedDocument);
            Count++;
        }

        public void Remove(TKey key)
        {
            _collection.Delete(Query.EQ("Identifier",
                Constants.IsNetCore ? JsonConvert.SerializeObject(key) : key.GetHashCode().ToString()));
            Count--;
        }

        /// <summary>
        /// Return's a mapped enumerable of <see cref="TValue" />
        /// </summary>
        public IEnumerable<TValue> Values
        {
            get
            {
                var documents = _collection.FindAll();
                return documents.Select(document =>
                    BsonMapper.Global.ToObject<StorageItem<TKey, TValue>>(document).Value);
            }
        }

        public void Clear()
        {
            _database.DropCollection(_collectionName);
            Count = 0;
        }
    }
}