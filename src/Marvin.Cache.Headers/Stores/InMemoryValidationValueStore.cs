﻿// Any comments, input: @KevinDockx
// Any issues, requests: https://github.com/KevinDockx/HttpCacheHeaders

using Marvin.Cache.Headers.Interfaces;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Marvin.Cache.Headers.Stores
{
    public class InMemoryValidationValueStore : IValidationValueStore
    {
        private readonly ConcurrentDictionary<string, ValidationValue> _store
            = new ConcurrentDictionary<string, ValidationValue>();

        public Task<ValidationValue> GetAsync(StoreKey key) => GetAsync(key.ToString());

        public Task SetAsync(StoreKey key, ValidationValue eTag) => SetAsync(key.ToString(), eTag);

        private Task<ValidationValue> GetAsync(string key)
        {
            return _store.ContainsKey(key) && _store[key] is ValidationValue eTag
                ? Task.FromResult(eTag)
                : Task.FromResult<ValidationValue>(null);
        }

        private Task SetAsync(string key, ValidationValue eTag)
        {
            _store[key] = eTag;
            return Task.FromResult(0);
        }
    }
}
