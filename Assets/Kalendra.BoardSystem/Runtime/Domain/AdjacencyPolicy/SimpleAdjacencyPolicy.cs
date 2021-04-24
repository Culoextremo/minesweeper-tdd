using System.Collections.Generic;
using Kalendra.BoardSystem.Runtime.Domain.Entities;

namespace Kalendra.BoardSystem.Runtime.Domain.AdjacencyPolicy
{
    public abstract class SimpleAdjacencyPolicy : IAdjacencyPolicy
    {
        public IEnumerable<ITile> GetAdjacentTiles(IBoard board, ITile tile)
        {
            if(tile is null)
                yield break;

            var (x, y) = GetAdjacentCoords(tile.Coords);
            
            if(!board.HasTile(x, y))
                yield break;
            
            yield return board[x, y];
        }

        protected abstract (int x, int y) GetAdjacentCoords((int x, int y) coords);
    }
    
    public class UpAdjacencyPolicy : SimpleAdjacencyPolicy
    {
        protected override (int x, int y) GetAdjacentCoords((int x, int y) coords) => (coords.x, coords.y + 1);
    }

    public class UpRightAdjacencyPolicy : SimpleAdjacencyPolicy
    {
        protected override (int x, int y) GetAdjacentCoords((int x, int y) coords) => (coords.x + 1, coords.y + 1);
    }
    
    public class RightAdjacencyPolicy : SimpleAdjacencyPolicy
    {
        protected override (int x, int y) GetAdjacentCoords((int x, int y) coords) => (coords.x + 1, coords.y);
    }
    
    public class RightDownAdjacencyPolicy : SimpleAdjacencyPolicy
    {
        protected override (int x, int y) GetAdjacentCoords((int x, int y) coords) => (coords.x + 1, coords.y - 1);
    }
    
    public class DownAdjacencyPolicy : SimpleAdjacencyPolicy
    {
        protected override (int x, int y) GetAdjacentCoords((int x, int y) coords) => (coords.x, coords.y - 1);
    }

    public class LeftDownAdjacencyPolicy : SimpleAdjacencyPolicy
    {
        protected override (int x, int y) GetAdjacentCoords((int x, int y) coords) => (coords.x - 1, coords.y - 1);
    }
    
    public class LeftAdjacencyPolicy : SimpleAdjacencyPolicy
    {
        protected override (int x, int y) GetAdjacentCoords((int x, int y) coords) => (coords.x - 1, coords.y);
    }
    
    public class UpLeftAdjacencyPolicy : SimpleAdjacencyPolicy
    {
        protected override (int x, int y) GetAdjacentCoords((int x, int y) coords) => (coords.x - 1, coords.y + 1);
    }
}