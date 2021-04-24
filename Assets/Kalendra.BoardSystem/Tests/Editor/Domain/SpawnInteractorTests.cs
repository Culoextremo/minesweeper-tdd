using System.Threading.Tasks;
using FluentAssertions;
using Kalendra.BoardSystem.Runtime.Domain.Entities.BoardOperations;
using Kalendra.BoardSystem.Runtime.Domain.UseCases;
using Kalendra.BoardSystem.Tests.TestDataBuilders.StaticShortcuts;
using Kalendra.Commons.Runtime.Architecture.Boundaries;
using Kalendra.Commons.Runtime.Architecture.Patterns;
using NSubstitute;
using NUnit.Framework;

namespace Kalendra.BoardSystem.Tests.Editor
{
    [Category("Integration")]
    public class SpawnInteractorTests
    {
        [Test]
        public async void Request_WhenBoardIsNotFull_SpawnsInEmptyTile()
        {
            //Arrange
            var someBoard = Build.Board().Build();
            
            IBoardAsyncOperation spawnOperation = Build.SpawnOperation().WithSpawnPolicy(Fake.SpawnOperatorPolicy()).Build();
            var mockFactory = Substitute.For<IFactory<SpawnOperation>>();
            mockFactory.Create().Returns(spawnOperation);
            
            var outputReceiverStub = Substitute.For<ISpawnUseCaseOutput>();
            IAsyncBoundaryInputPort sut = new SpawnUseCaseInteractor(someBoard, mockFactory, outputReceiverStub);
            
            //Act
            var resultEmptyTilesBefore = someBoard.ListAllEmptyTiles;
            sut.Request();
            await Task.Delay(1);
            var resultEmptyTilesAfter = someBoard.ListAllEmptyTiles;
            
            //Assert
            resultEmptyTilesBefore.Should().NotBeEmpty();
            resultEmptyTilesAfter.Should().BeEmpty();
        }
        
        [Test]
        public async void Request_WhenBoardIsNotFull_CallsReceiverAfterSpawn()
        {
            //Arrange
            var someBoard = Build.Board().Build();
            
            IBoardAsyncOperation spawnOperation = Build.SpawnOperation().WithSpawnPolicy(Fake.SpawnOperatorPolicy()).Build();
            var mockFactory = Substitute.For<IFactory<SpawnOperation>>();
            mockFactory.Create().Returns(spawnOperation);
            
            var outputReceiverStub = Substitute.For<ISpawnUseCaseOutput>();
            IAsyncBoundaryInputPort sut = new SpawnUseCaseInteractor(someBoard, mockFactory, outputReceiverStub);
            
            //Act
            sut.Request();
            
            //Assert
            await Task.Delay(1);
            outputReceiverStub.Received().Response();
        }

        [Test]
        public async void Request_WhenBoardIsFull_DoesNotCallsReceiverAfterSpawn()
        {
            //Arrange
            var someBoard = Build.Board_WithNoTiles();
            
            IBoardAsyncOperation spawnOperation = Build.SpawnOperation().WithSpawnPolicy(Fake.SpawnOperatorPolicy()).Build();
            var mockFactory = Substitute.For<IFactory<SpawnOperation>>();
            mockFactory.Create().Returns(spawnOperation);
            
            var outputReceiverStub = Substitute.For<ISpawnUseCaseOutput>();
            IAsyncBoundaryInputPort sut = new SpawnUseCaseInteractor(someBoard, mockFactory, outputReceiverStub);
            
            //Act
            sut.Request();
            
            //Assert
            await Task.Delay(1);
            outputReceiverStub.DidNotReceive().Response();
        }
        
        [Test]
        public async void Request_WhenBoardIsFull_CallsNotAvailableReceiverAfterSpawn()
        {
            //Arrange
            var someBoard = Build.Board_WithNoTiles();
            
            IBoardAsyncOperation spawnOperation = Build.SpawnOperation().WithSpawnPolicy(Fake.SpawnOperatorPolicy()).Build();
            var mockFactory = Substitute.For<IFactory<SpawnOperation>>();
            mockFactory.Create().Returns(spawnOperation);
            
            var outputReceiverStub = Substitute.For<ISpawnUseCaseOutput>();
            var outputNotAvailableReceiverStub = Substitute.For<ISpawnNotAvailableUseCaseOutput>();
            IAsyncBoundaryInputPort sut = new SpawnUseCaseInteractor(someBoard, mockFactory, outputReceiverStub, outputNotAvailableReceiverStub);
            
            //Act
            sut.Request();
            await Task.Delay(1);
            
            //Assert
            outputNotAvailableReceiverStub.Received().Response();
            outputReceiverStub.DidNotReceive().Response();
        }
    }
}