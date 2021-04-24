using System;
using System.Threading.Tasks;
using FluentAssertions;
using Kalendra.BoardSystem.Runtime.Domain.Entities.BoardOperations;
using Kalendra.BoardSystem.Tests.TestDataBuilders.StaticShortcuts;
using NUnit.Framework;
using Fake = Kalendra.BoardSystem.Tests.TestDataBuilders.StaticShortcuts.Fake;

namespace Kalendra.BoardSystem.Tests.Editor
{
    public class SpawnOperationTests
    {
        #region IsAvailable
        [Test]
        public void IsAvailable_IfBoardHasAnyEmptyTile()
        {
            var someEmptyBoard = Build.Board().Build();
            SpawnOperation sut = Build.SpawnOperation();

            var result = sut.IsAvailable(someEmptyBoard);

            result.Should().BeTrue();
        }

        [Test]
        public void IsNotAvailable_IfBoardHasNotAnyEmptyTile()
        {
            var someFullBoard = Build.Board_WithNoTiles();
            SpawnOperation sut = Build.SpawnOperation();

            var result = sut.IsAvailable(someFullBoard);

            result.Should().BeFalse();
        }
        #endregion

        #region Execute
        [Test]
        public async void Execute_WhenIsNotAvailable_ThrowsException()
        {
            //Arrange
            var someFullBoard = Build.Board().Build();
            someFullBoard.GetTile(0, 0).Content = Fake.TileContent_NotNull();

            var mockSpawnPolicy = Fake.SpawnOperatorPolicy();
            SpawnOperation sut = Build.SpawnOperation().WithSpawnPolicy(mockSpawnPolicy);
            
            //Act
            var act = new Func<Task>(async() => await sut.Execute(someFullBoard));

            //Assert
            act.Should().Throw<InvalidOperationException>();
            await act.Should().ThrowAsync<InvalidOperationException>();
        }

        [Test]
        public async void Execute_WhenIsAvailable_SpawnsContent()
        {
            //Arrange
            var someEmptyBoard = Build.Board().Build();

            var mockSpawnPolicy = Fake.SpawnOperatorPolicy();
            SpawnOperation sut = Build.SpawnOperation().WithSpawnPolicy(mockSpawnPolicy);
            
            //Act
            await sut.Execute(someEmptyBoard);

            //Assert
            var expectedSpawnedContent = await mockSpawnPolicy.SpawnContent();
            var resultSpawnedContent = someEmptyBoard.GetTile(0, 0).Content;
            
            resultSpawnedContent.Should().Be(expectedSpawnedContent);
        }
        
        [Test]
        public async void Execute_Twice_SpawnsSameContentInSameTile()
        {
            //Arrange
            var someEmptyBoard = Build.Board().Build();
            
            var randomSpawnPolicy = Fake.SpawnOperatorPolicy();
            SpawnOperation sut = Build.SpawnOperation().WithSpawnPolicy(randomSpawnPolicy);
            
            //Act
            await sut.Execute(someEmptyBoard);
            var resultContentBefore = someEmptyBoard.GetTile(0, 0).Content;
            await sut.Undo(someEmptyBoard);
            await sut.Execute(someEmptyBoard);
            var resultContentAfter = someEmptyBoard.GetTile(0, 0).Content;
            
            //Assert
            resultContentAfter.Should().Be(resultContentBefore);
        }
        #endregion
        
        #region Undo
        [Test]
        public async void Undo_WhenNotExecutedBefore_ThrowsException()
        {
            //Arrange
            var someBoard = Build.Board().Build();

            var mockSpawnPolicy = Fake.SpawnOperatorPolicy();
            SpawnOperation sut = Build.SpawnOperation().WithSpawnPolicy(mockSpawnPolicy);
            
            //Act
            var act = new Func<Task>(async() => await sut.Undo(someBoard));

            //Assert
            await act.Should().ThrowAsync<InvalidOperationException>();
        }
        
        [Test]
        public async void Undo_AfterExecuted_RecoversTileContent()
        {
            //Arrange
            var someEmptyBoard = Build.Board().Build();

            var mockSpawnPolicy = Fake.SpawnOperatorPolicy();
            SpawnOperation sut = Build.SpawnOperation().WithSpawnPolicy(mockSpawnPolicy);
            
            //Act
            var resultContentBefore = someEmptyBoard.GetTile(0, 0).Content;
            await sut.Execute(someEmptyBoard);
            var resultContentInBetween = someEmptyBoard.GetTile(0, 0).Content;
            await sut.Undo(someEmptyBoard);
            var resultContentAfter = someEmptyBoard.GetTile(0, 0).Content;

            //Assert
            var expectedSpawnedContent = await mockSpawnPolicy.SpawnContent();

            resultContentAfter.Should().Be(resultContentBefore);
            resultContentAfter.Should().NotBe(resultContentInBetween);
            resultContentInBetween.Should().Be(expectedSpawnedContent);
        }
        #endregion
    }
}