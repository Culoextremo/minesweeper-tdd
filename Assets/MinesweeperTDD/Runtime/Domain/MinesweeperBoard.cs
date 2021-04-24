using Kalendra.BoardSystem.Runtime.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinesweeperTDD.Tests
{
    public class MinesweeperBoard : IBoard
    {
        public ITile this[int i, int j] => throw new NotImplementedException();

        public (int x, int y) Size => throw new NotImplementedException();

        public IEnumerable<(int x, int y)> ListAllEmptyTiles => throw new NotImplementedException();

        public bool AddTile(int i, int j)
        {
            throw new NotImplementedException();
        }

        public ITile GetTile(int i, int j)
        {
            throw new NotImplementedException();
        }

        public bool HasTile(int i, int j)
        {
            throw new NotImplementedException();
        }

        public bool RemoveTile(int i, int j)
        {
            throw new NotImplementedException();
        }
    }
}
