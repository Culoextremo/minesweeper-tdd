using System;
using System.Collections.Generic;
using System.Linq;

namespace Kalendra.BoardSystem.Runtime.Domain.Entities
{
    public class Board : IBoard
    {
        public (int x, int y) Size { get; private set; }
        protected readonly Dictionary<(int x, int y), ITile> tiles = new Dictionary<(int, int), ITile>();
        
        #region Constructors/Init
        public Board(int sizeX, int sizeY)
        {
            AssertPositiveSize(sizeX, sizeY);
            
            Size = (sizeX, sizeY);
            InitTiles();
        }

        static void AssertPositiveSize(int sizeX, int sizeY)
        {
            if(sizeX < 1 || sizeY < 1)
                throw new ArgumentOutOfRangeException();
        }

        void InitTiles()
        {
            var (sizeX, sizeY) = Size;

            for(var i = 0; i < sizeX; i++)
            for(var j = 0; j < sizeY; j++)
                tiles[(i, j)] = new BoardTile(i, j);
        }
        #endregion

        #region Tile manipulation
        public ITile GetTile(int i, int j)
        {
            return HasTile(i, j) ? tiles[(i, j)] : new NullTile();
        }

        public bool HasTile(int i, int j)
        {
            return tiles.ContainsKey((i, j)) && !(tiles[(i, j)] is NullTile);
        }

        public bool RemoveTile(int i, int j)
        {
            if(!HasTile(i, j) || GetTile(i, j) is NullTile)
                return false;
            
            tiles[(i, j)] = new NullTile();
            RefreshBoardSize();
            return true;
        }

        public bool AddTile(int i, int j)
        {
            if(HasTile(i, j))
                return false;

            tiles[(i, j)] = new BoardTile(i, j);
            RefreshBoardSize();
            return true;
        }
        #endregion
        
        #region Autosize
        void RefreshBoardSize()
        {
            var noNullTiles = FindNoNullTiles().ToList();
            if(!noNullTiles.Any())
            {
                Size = (0, 0);
                return;
            }
            
            var xMax = noNullTiles.Select(pair => pair.Key.x).Max();
            var yMax = noNullTiles.Select(pair => pair.Key.y).Max();

            Size = (xMax + 1, yMax + 1);
        }
        #endregion

        public IEnumerable<(int x, int y)> ListAllEmptyTiles => FindNoNullTiles().Where(pair => pair.Value.Content is NullTileContent).Select(pair => pair.Key).ToList();
        protected IEnumerable<KeyValuePair<(int x, int y), ITile>> FindNoNullTiles() => tiles.Where(pair => !(pair.Value is NullTile));

        #region Operator overloading
        public ITile this[int x, int y] => GetTile(x, y);
        #endregion
    }
}