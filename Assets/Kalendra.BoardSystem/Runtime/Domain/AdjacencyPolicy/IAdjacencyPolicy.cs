using System.Collections.Generic;
using Kalendra.BoardSystem.Runtime.Domain.Entities;

namespace Kalendra.BoardSystem.Runtime.Domain.AdjacencyPolicy
{
    public interface IAdjacencyPolicy
    {
        IEnumerable<ITile> GetAdjacentTiles(IBoard board, ITile tile);
    }
}