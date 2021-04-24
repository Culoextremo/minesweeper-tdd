using Kalendra.Commons.Runtime.Architecture.Patterns;

namespace Kalendra.BoardSystem.Runtime.Domain.Entities.BoardOperations
{
    public interface IBoardOperation : ICommand<IBoard>
    {
        bool IsAvailable(IBoard targetBoard);
    }
    
    public interface IBoardAsyncOperation : ICommandAsync<IBoard>
    {
        bool IsAvailable(IBoard targetBoard);
        
        //TODO: somehow will return (or store after execute) BoardOperationResult.
        //Task Execute(IBoard targetBoard);
    }
}