using System.Collections.Generic;
using Kalendra.BoardSystem.Runtime.Domain.Entities;

namespace Kalendra.BoardSystem.Runtime.Domain.BoardQueries
{
    public class RowTilesBoardQuery : IBoardQuery<TilesBoardQueryResult>
    {
        readonly int row;

        public RowTilesBoardQuery(int row)
        {
            this.row = row;
        }

        public TilesBoardQueryResult Request(IBoard board)
        {
            var tilesinRow = new List<ITile>();
            
            for(var column = 0; column < board.Size.y; column++)
                if(board.HasTile(row, column))
                    tilesinRow.Add(board[row, column]);

            return new TilesBoardQueryResult(tilesinRow);
        }
    }
}