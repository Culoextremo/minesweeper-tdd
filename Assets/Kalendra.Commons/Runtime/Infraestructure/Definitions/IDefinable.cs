namespace Kalendra.Commons.Runtime.Infraestructure.Definitions
{
    /// <summary>
    /// As an alternative to <see cref="DefinitionScriptObj{TDataModel,TDefined}"/>,
    /// where you may not force your data models being children of <see cref="DefinitionDataModel"/>.
    /// </summary>
    /// <typeparam name="TDefined">Any data model this class will convert itself to.</typeparam>
    public interface IDefinable<out TDefined>
    {
        TDefined ToDefined();
    }
}