using System;
using System.Threading.Tasks;

namespace Kalendra.BoardSystem.Runtime.Domain.Entities.BoardOperations
{
    class AddTileBoardOperation : IBoardAsyncOperation
    {
        public (int x, int y) Coords { get; }

        public AddTileBoardOperation(int x, int y)
        {
            Coords = (x, y);
        }

        public Task Execute(IBoard targetBoard)
        {
            if(!IsAvailable(targetBoard))
                throw new InvalidOperationException("Can't add a previously existing tile.");

            targetBoard.AddTile(Coords.x, Coords.y);
            return Task.CompletedTask;
        }

        //TODO: save state so just undo if it was the same exactly tile & board.
        public Task Undo(IBoard targetBoard)
        {
            targetBoard.RemoveTile(Coords.x, Coords.y);
            return Task.CompletedTask;
        }

        public bool IsAvailable(IBoard targetBoard)
        {
            return !targetBoard.HasTile(Coords.x, Coords.y);
        }
    }
}