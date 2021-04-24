using Kalendra.Commons.Tests.TestDataBuilders.Builders;
using MinesweeperTDD.Runtime.Domain;

namespace MinesweeperTDD.Tests.TestDataBuilders
{
    public class MinesweeperContentBuilder : Builder<MinesweeperContent>
    {
        bool isBomb;

        #region Fluent API
        public MinesweeperContentBuilder WithIsBomb(bool isBomb)
        {
            this.isBomb = isBomb;
            return this;
        }
        #endregion

        #region ObjectMother/FactoryMethods
        MinesweeperContentBuilder()
        {
        }

        public static MinesweeperContentBuilder New() => new MinesweeperContentBuilder();
        #endregion

        #region Builder implementation
        public override MinesweeperContent Build() => new MinesweeperContent(isBomb);
        #endregion
    }
}