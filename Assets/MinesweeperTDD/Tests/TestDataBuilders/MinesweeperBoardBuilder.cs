using Kalendra.Commons.Tests.TestDataBuilders.Builders;
using MinesweeperTDD.Runtime.Domain;

namespace MinesweeperTDD.Tests.TestDataBuilders
{
    public class MinesweeperBoardBuilder : Builder<MinesweeperBoard>
    {
        int sizeX = 1;
        int sizeY = 1;
        int bombCount = 1;

        #region Fluent API
        public MinesweeperBoardBuilder WithSize(int sizeX, int sizeY)
        {
            this.sizeX = sizeX;
            this.sizeY = sizeY;
            return this;
        }
        public MinesweeperBoardBuilder WithBombCount(int bombCount)
        {
            this.bombCount = bombCount;
            return this;
        }
        #endregion

        #region ObjectMother/FactoryMethods
        MinesweeperBoardBuilder()
        {
        }

        public static MinesweeperBoardBuilder New() => new MinesweeperBoardBuilder();
        #endregion

        #region Builder implementation
        public override MinesweeperBoard Build() => new MinesweeperBoard(sizeX, sizeY, bombCount);
        #endregion
    }
}