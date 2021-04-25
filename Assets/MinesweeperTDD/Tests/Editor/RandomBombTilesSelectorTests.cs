
using FluentAssertions;
using Kalendra.BoardSystem.Runtime.Domain.Entities;
using Kalendra.Commons.Runtime.Architecture.Services;
using Kalendra.Commons.Runtime.Infraestructure.Services;
using NUnit.Framework;
using Kalendra.BoardSystem.Tests.TestDataBuilders.StaticShortcuts;

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
    }
}
