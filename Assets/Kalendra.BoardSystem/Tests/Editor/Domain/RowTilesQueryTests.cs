using System.Linq;
using FluentAssertions;
using Kalendra.BoardSystem.Runtime.Domain.BoardQueries;
using Kalendra.BoardSystem.Tests.TestDataBuilders.StaticShortcuts;
using NUnit.Framework;

namespace Kalendra.BoardSystem.Tests.Editor
{
    public class RowTilesQueryTests
    {
        [Test]
        public void RequestRow_OutOfRange_ReturnsEmptyEnumerator()
        {
            var someboard = Build.Board().Build();
            var sut = new RowTilesBoardQuery(1);

            var result = sut.Request(someboard);

            result.tiles.Should().BeEmpty();
        }
        
        [Test]
        public void RequestFirstRow_OneTiledBoard_ReturnsFirstTile()
        {
            var someBoard = Build.Board().Build();
            var sut = new RowTilesBoardQuery(0);

            var result = sut.Request(someBoard);

            result.tiles.Single().Should().Be(someBoard[0, 0]);
        }

        [Test]
        public void RequestFirstRow_OneRowBoard_ReturnsAllTiles()
        {
            var someBoard = Build.Board().WithSize(1, 2).Build();
            var sut = new RowTilesBoardQuery(0);

            var result = sut.Request(someBoard);

            result.tiles.Should().ContainInOrder(someBoard[0, 0], someBoard[0, 1]);
        }

        [Test]
        public void RequestUncompletedRow_JustReturnsExistingTiles()
        {
            //Arrange
            var someBoard = Build.Board().WithSize(1, 3).Build();
            var removedTile = someBoard[0, 1];
            someBoard.RemoveTile(0, 1);
            
            var sut = new RowTilesBoardQuery(0);

            //Act
            var result = sut.Request(someBoard);

            //Assert
            result.tiles.Should().NotContain(removedTile);
            result.tiles.Should().ContainInOrder(someBoard[0, 0], someBoard[0, 2]);
        }

        [Test]
        public void RequestInnerNotExistingRow_ReturnsEmptyEnumerator()
        {
            //Arrange
            var someBoard = Build.Board().WithSize(3, 1).Build();
            someBoard.RemoveTile(1, 0);

            var sut = new RowTilesBoardQuery(1);
            
            //Act
            var result = sut.Request(someBoard);
            
            //Assert
            result.tiles.Should().BeEmpty();
        }
    }
}