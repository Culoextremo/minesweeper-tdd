using Kalendra.BoardSystem.Runtime.Domain.Entities.BoardOperations;
using Kalendra.BoardSystem.Runtime.Domain.Policy;
using Kalendra.Commons.Tests.TestDataBuilders.Builders;
using NSubstitute;

namespace Kalendra.BoardSystem.Tests.TestDataBuilders.Domain
{
    internal class SpawnOperationBuilder : Builder<SpawnOperation>
    {
        ISpawnOperatorPolicy spawnPolicy = Substitute.For<ISpawnOperatorPolicy>();

        SpawnOperationBuilder() { }

        #region Fluent API
        public SpawnOperationBuilder WithSpawnPolicy(ISpawnOperatorPolicy spawnPolicy)
        {
            this.spawnPolicy = spawnPolicy;
            return this;
        }
        #endregion
        
        #region ObjectMother/FactoryMethods
        public static SpawnOperationBuilder New() => new SpawnOperationBuilder();
        #endregion

        #region Builder implementation
        public override SpawnOperation Build() => new SpawnOperation(spawnPolicy);
        #endregion
    }
}