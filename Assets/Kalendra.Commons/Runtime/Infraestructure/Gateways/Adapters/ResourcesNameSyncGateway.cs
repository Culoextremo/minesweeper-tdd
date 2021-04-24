using System.Collections.Generic;
using System.Threading.Tasks;
using Kalendra.Commons.Runtime.Architecture.Gateways;
using UnityEngine;

namespace Kalendra.Commons.Runtime.Infraestructure.Gateways.Adapters
{
    //TODO: test Load<T>.
    public class ResourcesNameSyncGateway<T> : IReadOnlyAsyncRepository<T> where T : Object
    {
        public async Task<IEnumerable<T>> LoadAll() 
        {
            var loadedCollection = Resources.LoadAll<T>("");
            return await Task.FromResult(loadedCollection);
        }

        public async Task<T> Load(string hashID)
        {
            var loaded = Resources.Load<T>(hashID);
            return await Task.FromResult(loaded);
        }
    }
}