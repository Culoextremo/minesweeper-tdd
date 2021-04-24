using System;

namespace Kalendra.BoardSystem.Runtime.Domain.Entities
{
    internal class BoardTile : ITile
    {
        public (int x, int y) Coords { get; }

        public bool IsEmpty => Content is NullTileContent;
        public ITileContent Content { get; set; } = new NullTileContent();

        #region Constructors/Init
        public BoardTile(int coordX, int coordY)
        {
            Coords = (coordX, coordY);
        }
        #endregion
    }
}