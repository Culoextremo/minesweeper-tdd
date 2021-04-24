using Kalendra.Commons.Runtime.Infraestructure.Definitions;
using Kalendra.Commons.Tests.TestDataBuilders.Builders;
using UnityEngine;

namespace Kalendra.Commons.Tests.TestDataBuilders.Infraestructure.CharacterTaxonomySystem
{
    internal abstract class DefinitionBuilder<T, TDataModel, TDefined> : Builder<T>
        where T : DefinitionScriptObj<TDataModel, TDefined>
        where TDataModel : DefinitionDataModel
    {
        protected abstract TDataModel BuildDataModel();
        
        public override T Build()
        {
            var instance = ScriptableObject.CreateInstance<T>();
            var dataModel = BuildDataModel();

            var instanceID = instance.GetType().GetField("dataModel",
                System.Reflection.BindingFlags.NonPublic | 
                System.Reflection.BindingFlags.Instance);
            instanceID.SetValue(instance, dataModel);

            return instance;
        }
    }
}