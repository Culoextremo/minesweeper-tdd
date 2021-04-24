using Kalendra.BoardSystem.Runtime.Domain.Entities;
using Kalendra.BoardSystem.Runtime.Domain.Entities.BoardOperations;
using Kalendra.BoardSystem.Runtime.Domain.Policy;
using Kalendra.Commons.Tests.TestDataBuilders.Builders;
using NSubstitute;

namespace Kalendra.BoardSystem.Tests.TestDataBuilders.Domain
{
    internal class MergeOperationBuilder : Builder<MergeOperation>
    {
        ITile originTile = Substitute.For<ITile>();
        ITile targetTile = Substitute.For<ITile>();
        
        IMergeOperatorPolicy policy = Substitute.For<IMergeOperatorPolicy>();

        #region Fluent API
        public MergeOperationBuilder WithPolicy(IMergeOperatorPolicy policy)
        {
            this.policy = policy;
            return this;
        }

        public MergeOperationBuilder WithOriginTile(ITile originTile)
        {
            this.originTile = originTile;
            return this;
        }

        public MergeOperationBuilder WithTargetTile(ITile targetTile)
        {
            this.targetTile = targetTile;
            return this;
        }
        #endregion

        #region ObjectMother/FactoryMethods
        public static MergeOperationBuilder New() => new MergeOperationBuilder();
        #endregion

        #region Builder implementation
        public override MergeOperation Build() => new MergeOperation(originTile, targetTile, policy);
        #endregion
    }
}