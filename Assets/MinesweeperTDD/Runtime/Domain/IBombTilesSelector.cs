using Kalendra.BoardSystem.Runtime.Domain.Entities;
using System.Collections.Generic;

namespace MinesweeperTDD.Tests
{
    public interface IBombTilesSelector
    {
        IEnumerable<(int, int)> GetBombTiles(IBoard board, int bombCount);
    }
}
