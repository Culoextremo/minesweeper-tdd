using System.Collections.Generic;
using System.Linq;
using Kalendra.BoardSystem.Runtime.Domain.Entities;

namespace Kalendra.BoardSystem.Runtime.Domain.AdjacencyPolicy
{
    public abstract class GroupAdjacencyPolicyTemplate : IAdjacencyPolicy
    {
        public IEnumerable<ITile> GetAdjacentTiles(IBoard board, ITile tile)
        {
            if(tile is null)
                return Enumerable.Empty<ITile>();

            var allCoords = GetAllAdjacentCoords(tile.Coords);

            return allCoords
                .Where(coords => board.HasTile(coords.x, coords.y))
                .Select(coords => board[coords.x, coords.y]);
        }

        protected abstract IEnumerable<(int x, int y)> GetAllAdjacentCoords((int x, int y) coords);
    }
}