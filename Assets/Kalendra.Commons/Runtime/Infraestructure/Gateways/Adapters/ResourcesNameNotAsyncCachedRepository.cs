using System.Collections.Generic;
using System.Threading.Tasks;
using Kalendra.Commons.Runtime.Architecture.Gateways;
using UnityEngine;

namespace Kalendra.Commons.Runtime.Infraestructure.Gateways.Adapters
{
    class ResourcesNameNotAsyncCachedRepository<T> : IReadOnlyAsyncRepository<T> where T : Object
    {
        readonly HashSet<T> cache = new HashSet<T>();
        bool hasLoadAllBefore;
        
        public async Task<IEnumerable<T>> LoadAll()
        {
            if(!hasLoadAllBefore)
                LoadAllInCache();
            
            return await Task.FromResult(cache);
        }

        public async Task<T> Load(string hashID)
        {
            var loaded = Resources.Load<T>(hashID);
            RefreshCache(loaded);
            return await Task.FromResult(loaded);
        }
        
        #region Cache
        void LoadAllInCache()
        {
            var loadedCollection = Resources.LoadAll<T>("");
            RefreshCache(loadedCollection);
            
            hasLoadAllBefore = true;
            
        }
        void RefreshCache(IEnumerable<T> loaded)
        {
            foreach(var loadedElement in loaded)
                RefreshCache(loadedElement);
        }

        void RefreshCache(T loaded) => cache.Add(loaded);
        #endregion
    }
}