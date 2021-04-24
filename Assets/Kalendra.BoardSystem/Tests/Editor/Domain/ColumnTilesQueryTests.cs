using System.Linq;
using FluentAssertions;
using Kalendra.BoardSystem.Runtime.Domain.BoardQueries;
using Kalendra.BoardSystem.Tests.TestDataBuilders.StaticShortcuts;
using NUnit.Framework;

namespace Kalendra.BoardSystem.Tests.Editor
{
    public class ColumnTilesQueryTests
    {
        [Test]
        public void RequestColumn_OutOfRange_ReturnsEmptyEnumerator()
        {
            var someBoard = Build.Board().Build();
            var sut = new ColumnTilesBoardQuery(1);

            var result = sut.Request(someBoard);

            result.tiles.Should().BeEmpty();
        }

        [Test]
        public void RequestFirstColumn_OneTiledBoard_ReturnsFirstTile()
        {
            var someBoard = Build.Board().Build();
            var sut = new ColumnTilesBoardQuery(0);

            var result = sut.Request(someBoard);

            result.tiles.Single().Should().Be(someBoard[0, 0]);
        }
        
        [Test]
        public void RequestFirstColumn_OneColumnBoard_ReturnsAllTiles()
        {
            var someBoard = Build.Board().WithSize(2, 1).Build();
            var sut = new ColumnTilesBoardQuery(0);

            var result = sut.Request(someBoard);

            result.tiles.Should().ContainInOrder(someBoard[0, 0], someBoard[1, 0]);
        }

        [Test]
        public void RequestUncompletedColumn_JustReturnsExistingTiles()
        {
            //Arrange
            var someBoard = Build.Board().WithSize(3, 1).Build();
            var removedTile = someBoard[1, 0];
            someBoard.RemoveTile(1, 0);
            
            var sut = new ColumnTilesBoardQuery(0);

            //Act
            var result = sut.Request(someBoard);

            //Assert
            result.tiles.Should().NotContain(removedTile);
            result.tiles.Should().ContainInOrder(someBoard[0, 0], someBoard[2, 0]);
        }

        [Test]
        public void RequestInnerNotExistingColumn_ReturnsEmptyEnumerator()
        {
            //Arrange
            var someBoard = Build.Board().WithSize(1, 3).Build();
            someBoard.RemoveTile(0, 1);

            var sut = new ColumnTilesBoardQuery(1);
            
            //Act
            var result = sut.Request(someBoard);
            
            //Assert
            result.tiles.Should().BeEmpty();
        }
    }
}