using System.Collections.Generic;
using Kalendra.Commons.Runtime.Architecture.Gateways;
using UnityEngine;

namespace Kalendra.Commons.Runtime.Infraestructure.Gateways.Adapters
{
    public class ResourcesNameRepository<T> : IReadRepository<T> where T : Object
    {
        public T Load(string hashID) => Resources.Load<T>(hashID);
        public IEnumerable<T> LoadAll() => Resources.LoadAll<T>("");
    }
}