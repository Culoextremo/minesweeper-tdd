using Kalendra.Commons.Runtime.Architecture.Boundaries;

namespace Kalendra.BoardSystem.Runtime.Domain.UseCases
{
    public interface ISpawnUseCaseInput : IAsyncBoundaryInputPort { }
    
    public interface ISpawnUseCaseOutput : IAsyncBoundaryOutputPort { }
    public interface ISpawnNotAvailableUseCaseOutput : IAsyncBoundaryOutputPort { }
}