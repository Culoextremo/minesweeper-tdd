using System.Collections.Generic;
using Kalendra.BoardSystem.Runtime.Domain.Entities;

namespace Kalendra.BoardSystem.Runtime.Domain.BoardQueries
{
    public interface IBoardQueryResult { }

    public class TilesBoardQueryResult : IBoardQueryResult
    {
        public readonly IReadOnlyCollection<ITile> tiles;

        public TilesBoardQueryResult(IReadOnlyCollection<ITile> tiles)
        {
            this.tiles = tiles;
        }
    }
}