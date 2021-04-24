using Kalendra.BoardSystem.Runtime.Domain.Entities;
using Kalendra.Commons.Tests.TestDataBuilders.Builders;

namespace Kalendra.BoardSystem.Tests.TestDataBuilders.Domain
{
    internal class BoardBuilder : Builder<Board>
    {
        int sizeX = 1, sizeY = 1;

        #region Fluent API
        public BoardBuilder WithSize(int sizeX, int sizeY)
        {
            this.sizeX = sizeX;
            this.sizeY = sizeY;
            
            return this;
        }
        #endregion

        #region ObjectMother/FactoryMethods
        public static BoardBuilder New() => new BoardBuilder();

        public static Board BoardWithNoTiles()
        {
            var boardObjectMother = new BoardBuilder().Build();
            boardObjectMother.RemoveTile(0, 0);
            return boardObjectMother;
        }
        #endregion

        #region Builder implementation
        public override Board Build() => new Board(sizeX, sizeY);
        #endregion
    }
}