
using System.Collections.Generic;
using Kalendra.BoardSystem.Runtime.Domain.Entities;

namespace Kalendra.BoardSystem.Runtime.Domain.BoardQueries
{
    public class ColumnTilesBoardQuery : IBoardQuery<TilesBoardQueryResult>
    {
        readonly int column;

        public ColumnTilesBoardQuery(int column)
        {
            this.column = column;
        }
        
        public TilesBoardQueryResult Request(IBoard board)
        {
            var tilesInColumn = new List<ITile>();
            
            for(var row = 0; row < board.Size.x; row++)
                if(board.HasTile(row, column))
                    tilesInColumn.Add(board[row, column]);

            return new TilesBoardQueryResult(tilesInColumn);
        }
    }
}