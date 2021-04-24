using System.Linq;
using Kalendra.BoardSystem.Runtime.Domain.Entities;

namespace Kalendra.BoardSystem.Runtime.Domain.BoardQueries
{
    public class IsFullBoardQuery : IBoardQuery<bool>
    {
        public bool Request(IBoard board) => !board.ListAllEmptyTiles.Any();
    }
}