using System.Collections.Generic;
using System.Linq;

namespace Kalendra.BoardSystem.Runtime.Domain.Entities.BoardOperations
{
    public class BoardAsyncOperationScheduler
    {
        readonly Queue<IBoardAsyncOperation> scheduled = new Queue<IBoardAsyncOperation>();
        readonly Stack<IBoardAsyncOperation> executed = new Stack<IBoardAsyncOperation>();
        readonly Stack<IBoardAsyncOperation> undone = new Stack<IBoardAsyncOperation>();

        public IEnumerable<IBoardAsyncOperation> Scheduled => scheduled;

        public bool HasScheduled => scheduled.Any();
        public bool HasExecuted => executed.Any();
        public bool HasUndone => undone.Any();


        public BoardAsyncOperationScheduler(ICollection<IBoardAsyncOperation> begginingOperations = null)
        {
            if(begginingOperations?.Any() ?? false)
                scheduled = new Queue<IBoardAsyncOperation>(begginingOperations);
        }
        
        public void RegisterOperation(IBoardAsyncOperation operation)
        {
            scheduled.Enqueue(operation);
        }

        public void DoNext(IBoard board)
        {
            if(scheduled.Count == 0)
                return;
            
            var nextOperation = scheduled.Dequeue();
            executed.Push(nextOperation);
            
            nextOperation.Execute(board);
        }

        public void UndoLast(IBoard board)
        {
            if(executed.Count == 0)
                return;
            
            var lastOperation = executed.Pop();
            undone.Push(lastOperation);

            lastOperation.Undo(board);
        }

        public void RedoLast(IBoard board)
        {
            if(undone.Count == 0)
                return;

            var lastUndoneOperation = undone.Pop();
            executed.Push(lastUndoneOperation);

            lastUndoneOperation.Execute(board);
        }
    }
}