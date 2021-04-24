using System.Threading.Tasks;
using Kalendra.BoardSystem.Runtime.Domain.Entities;
using Kalendra.BoardSystem.Runtime.Domain.Entities.BoardOperations;
using Kalendra.Commons.Runtime.Architecture.Patterns;

namespace Kalendra.BoardSystem.Runtime.Domain.UseCases
{
    public class SpawnUseCaseInteractor : ISpawnUseCaseInput
    {
        readonly IBoard entityBoard;
        readonly IFactory<SpawnOperation> spawnFactory;

        readonly ISpawnUseCaseOutput useCaseOutputBoundary;
        readonly ISpawnNotAvailableUseCaseOutput useCaseOutputNotAvailableUseCaseBoundary;
        
        public SpawnUseCaseInteractor(IBoard entityBoard, IFactory<SpawnOperation> spawnFactory, ISpawnUseCaseOutput useCaseOutputBoundary, ISpawnNotAvailableUseCaseOutput useCaseOutputNotAvailableUseCaseBoundary = null)
        {
            this.entityBoard = entityBoard;
            this.spawnFactory = spawnFactory;

            this.useCaseOutputBoundary = useCaseOutputBoundary;
            this.useCaseOutputNotAvailableUseCaseBoundary = useCaseOutputNotAvailableUseCaseBoundary;
        }

        public async Task Request()
        {
            var spawnOperation = spawnFactory.Create();

            if(spawnOperation.IsAvailable(entityBoard))
                await LaunchSpawnUseCase(spawnOperation);
            else
                ResponseNotAvailableUseCase();
        }

        async Task LaunchSpawnUseCase(SpawnOperation spawnOperation)
        {
            //TODO: operations command center, to let Undo after.
            await spawnOperation.Execute(entityBoard);
            useCaseOutputBoundary.Response();
        }

        void ResponseNotAvailableUseCase()
        {
            useCaseOutputNotAvailableUseCaseBoundary?.Response();
        }
    }
}