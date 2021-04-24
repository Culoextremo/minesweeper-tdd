using System;
using System.Collections.Generic;
using Kalendra.Commons.Runtime.Architecture.Gateways;
using UnityEngine;

namespace Kalendra.Commons.Runtime.Infraestructure.Gateways.Adapters
{
    class CachedRepositoryDecorator<T> : IReadRepository<T>
    {
        readonly HashSet<T> cacheBunch = new HashSet<T>();
        readonly Dictionary<string, T> cache = new Dictionary<string, T>();
        
        readonly IReadRepository<T> decorated;
        public CachedRepositoryDecorator(IReadRepository<T> decorated) => this.decorated = decorated;

        public T Load(string hashID)
        {
            if(!cache.ContainsKey(hashID))
                cache[hashID] = decorated.Load(hashID);
            
            return cache[hashID];
        }

        public IEnumerable<T> LoadAll()
        {
            if(cacheBunch.Count == 0)
                LoadCacheBunch();

            return cacheBunch;
        }

        #region Support methods
        void LoadCacheBunch()
        {
            foreach(var loadedElement in decorated.LoadAll())
                cacheBunch.Add(loadedElement);
        }
        #endregion
    }
}