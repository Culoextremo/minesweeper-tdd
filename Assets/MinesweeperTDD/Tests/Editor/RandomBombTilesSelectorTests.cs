
using System.Linq;
using FluentAssertions;
using Kalendra.Commons.Runtime.Infraestructure.Services;
using NUnit.Framework;
using Kalendra.BoardSystem.Tests.TestDataBuilders.StaticShortcuts;
using Kalendra.Commons.Runtime.Architecture.Services;
using NSubstitute;

namespace MinesweeperTDD.Tests
{
    class RandomBombTilesSelectorTests
    {
        [Test]
        public void GetBombTiles_WhenSizeIsGreaterThanOne_ReturnsAtLeastOne()
        {
            var randomService = new SystemRandomService();
            var board = Build.Board().WithSize(1, 2).Build();
            RandomBombTilesSelector sut = new RandomBombTilesSelector(randomService);

            var result = sut.GetBombTiles(board, 1);

            result.Should().NotBeNullOrEmpty();
        }

        [Test]
        public void GetBombTiles_WhenEnoughSize_ReturnsFullCount()
        {
            var randomService = new SystemRandomService();
            var board = Build.Board().WithSize(5, 5).Build();
            RandomBombTilesSelector sut = new RandomBombTilesSelector(randomService);

            var result = sut.GetBombTiles(board, 3);

            result.Should().HaveCount(3);
        }

        [Test]
        public void GetBombTiles_WhenNotEnoughSize_ReturnsClampedCount()
        {
            var randomService = new SystemRandomService();
            var board = Build.Board().WithSize(1, 3).Build();
            RandomBombTilesSelector sut = new RandomBombTilesSelector(randomService);

            var result = sut.GetBombTiles(board, 2);

            result.Should().HaveCountLessThan(board.Size.x * board.Size.y);
        }

        [Test]
        public void GetBombTiles_Tiles_AreUnique()
        {
            var randomService = new SystemRandomService();
            var board = Build.Board().WithSize(1, 3).Build();
            RandomBombTilesSelector sut = new RandomBombTilesSelector(randomService);

            var result = sut.GetBombTiles(board, 2);

            result.Should().OnlyHaveUniqueItems();
        }

        [Test]
        public void GetBombTiles_SelectTiles_ByRandomService()
        {
            //Arrange
            var randomMock = Substitute.For<IRandomService>();
            randomMock.GetRandom<(int, int)>(default).ReturnsForAnyArgs((0, 1));

            var board = Build.Board().WithSize(1, 2).Build();
            RandomBombTilesSelector sut = new RandomBombTilesSelector(randomMock);
            
            //Act       
            var result = sut.GetBombTiles(board, 1);

            //Assert
            result.Should().Contain((0, 1));
        }
    }
}
