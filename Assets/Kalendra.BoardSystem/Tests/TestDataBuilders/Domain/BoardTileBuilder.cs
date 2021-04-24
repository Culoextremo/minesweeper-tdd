using Kalendra.BoardSystem.Runtime.Domain.Entities;
using Kalendra.Commons.Tests.TestDataBuilders.Builders;

namespace Kalendra.BoardSystem.Tests.TestDataBuilders.Domain
{
    internal class BoardTileBuilder : Builder<BoardTile>
    {
        int coordX, coordY;        

        #region Fluent API
        public BoardTileBuilder WithCoords(int x, int y)
        {
            coordX = x;
            coordY = y;
            
            return this;
        }
        #endregion

        #region ObjectMother/FactoryMethods
        public static BoardTileBuilder New() => new BoardTileBuilder();
        #endregion

        #region Builder implementation
        public override BoardTile Build() => new BoardTile(coordX, coordY);
        #endregion
    }
}