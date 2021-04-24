using System.Collections.Generic;
using Kalendra.Commons.Runtime.Application.Serialization;
using Kalendra.Commons.Runtime.Architecture.Gateways;
using UnityEngine;

namespace Kalendra.Commons.Runtime.Infraestructure.Gateways.Adapters
{
    public class SerializerCachedRepository<T> : IRepository<T> where T : class
    {
        readonly ISerializer<T> serializer;

        public SerializerCachedRepository(ISerializer<T> serializer) => this.serializer = serializer;

        #region Cache
        static readonly Dictionary<string, object> cache = new Dictionary<string, object>();
        public void ClearCache() => cache.Clear();
        #endregion
        
        #region Hooks
        protected virtual string HashToSavingName(string hashID) => typeof(T).Name + "#" + hashID;
        #endregion
        
        #region Load
        public T Load(string hashID) 
        {
            var savingName = HashToSavingName(hashID);
            
            //Already in cache.
            if(cache.ContainsKey(savingName))
                return cache[savingName] as T;

            //Not in cache.
            var json = PlayerPrefs.GetString(savingName);

            T loaded = null;
            if(!string.IsNullOrEmpty(json))
            {
                loaded = serializer.Deserialize(json);
                cache[savingName] = loaded;
            }

            return loaded;
        }
        #endregion
        
        #region Save
        public void Save(T targetToSave, string hashID = "")
        {
            var savingName = HashToSavingName(hashID);
            
            //Reminder: []operator updates or adds if is not.
            cache[savingName] = targetToSave;

            var json = serializer.Serialize(targetToSave);
            PlayerPrefs.SetString(savingName, json);
        }
        #endregion
        
        #region Delete
        public bool Delete(string hashID)
        {
            var hasKey = cache.ContainsKey(hashID) || PlayerPrefs.HasKey(hashID);
            
            cache.Remove(hashID);
            PlayerPrefs.DeleteKey(hashID);
            
            return hasKey;
        }
        #endregion
    }
}