using System.Linq;
using FluentAssertions;
using Kalendra.BoardSystem.Runtime.Domain.AdjacencyPolicy;
using Kalendra.BoardSystem.Tests.TestDataBuilders.StaticShortcuts;
using NUnit.Framework;

namespace Kalendra.BoardSystem.Tests.Editor
{
    public class SquareAdjacencyTests
    {
        [Test]
        public void SquareAdjacency_OutOfRange_ReturnsEmpty()
        {
            var board = Build.Board().Build();
            SquareAdjacencyPolicy sut = new SquareAdjacencyPolicy();

            var result = sut.GetAdjacentTiles(board, board[0, 0]);

            result.Should().BeEmpty();
        }

        [Test]
        public void SquareAdjacency_InnerTile_ReturnsEightAdjacents_Clockwise()
        {
            var board = Build.Board().WithSize(3, 3).Build();
            SquareAdjacencyPolicy sut = new SquareAdjacencyPolicy();

            var result = sut.GetAdjacentTiles(board, board[1, 1])
                .Select(tile => tile.Coords);

            result.Should().HaveCount(8);
            result.Should().ContainInOrder((1, 2), (2, 2), (2, 1), (2, 0), (1, 0), (0, 0), (0, 1), (0, 2));
        }

        [Test]
        public void SquareAdjacency_InnerLeftTile_ReturnsFiveAdjacents_ClockwiseNoLeftwards()
        {
            var board = Build.Board().WithSize(3, 3).Build();
            SquareAdjacencyPolicy sut = new SquareAdjacencyPolicy();

            var result = sut.GetAdjacentTiles(board, board[0, 1])
                .Select(tile => tile.Coords);

            result.Should().HaveCount(5);
            result.Should().ContainInOrder((0, 2), (1, 2), (1, 1), (1, 0), (0, 0));
        }

        [Test]
        public void SquareAdjacency_InnerRightTile_ReturnsFiveAdjacents_ClockwiseNoRightwards()
        {
            var board = Build.Board().WithSize(3, 3).Build();
            SquareAdjacencyPolicy sut = new SquareAdjacencyPolicy();

            var result = sut.GetAdjacentTiles(board, board[2, 1])
                .Select(tile => tile.Coords);

            result.Should().HaveCount(5);
            result.Should().ContainInOrder((2, 2), (2, 0), (1, 0), (1, 1), (1, 2));
        }

        [Test]
        public void SquareAdjacency_InnerTopTile_ReturnsFiveAdjacents_ClockwiseNoAbove()
        {
            var board = Build.Board().WithSize(3, 3).Build();
            SquareAdjacencyPolicy sut = new SquareAdjacencyPolicy();

            var result = sut.GetAdjacentTiles(board, board[1, 2])
                .Select(tile => tile.Coords);

            result.Should().HaveCount(5);
            result.Should().ContainInOrder((2, 2), (2, 1), (1, 1), (0, 1), (0, 2));
        }

        [Test]
        public void SquareAdjacency_InnerDownTile_ReturnsFiveAdjacents_ClockwiseNoBelow()
        {
            var board = Build.Board().WithSize(3, 3).Build();
            SquareAdjacencyPolicy sut = new SquareAdjacencyPolicy();

            var result = sut.GetAdjacentTiles(board, board[1, 0])
                .Select(tile => tile.Coords);

            result.Should().HaveCount(5);
            result.Should().ContainInOrder((1, 1), (2, 1), (2, 0), (0, 0), (0, 1));
        }

        [Test]
        public void SquareAdjacency_TopLeftTile_ReturnsThreeAdjacents_ClockwiseRightToDown()
        {
            var board = Build.Board().WithSize(3, 3).Build();
            SquareAdjacencyPolicy sut = new SquareAdjacencyPolicy();

            var result = sut.GetAdjacentTiles(board, board[0, 2])
                .Select(tile => tile.Coords);

            result.Should().HaveCount(3);
            result.Should().ContainInOrder((1, 2), (1, 1), (0, 1));
        }

        [Test]
        public void SquareAdjacency_TopRightTile_ReturnsThreeAdjacents_ClockwiseDownToLeft()
        {
            var board = Build.Board().WithSize(3, 3).Build();
            SquareAdjacencyPolicy sut = new SquareAdjacencyPolicy();

            var result = sut.GetAdjacentTiles(board, board[2, 2])
                .Select(tile => tile.Coords);

            result.Should().HaveCount(3);
            result.Should().ContainInOrder((2, 1), (1, 1), (1, 2));
        }

        [Test]
        public void SquareAdjacency_BottomLeftTile_ReturnsThreeAdjacents_ClockwiseUpAndRight()
        {
            var board = Build.Board().WithSize(3, 3).Build();
            SquareAdjacencyPolicy sut = new SquareAdjacencyPolicy();

            var result = sut.GetAdjacentTiles(board, board[0, 0])
                .Select(tile => tile.Coords);

            result.Should().HaveCount(3);
            result.Should().ContainInOrder((0, 1), (1, 1), (1, 0));
        }

        [Test]
        public void SquareAdjacency_BottomRightTile_ReturnsThreeAdjacents_ClockwiseUpAndLeft()
        {
            var board = Build.Board().WithSize(3, 3).Build();
            SquareAdjacencyPolicy sut = new SquareAdjacencyPolicy();

            var result = sut.GetAdjacentTiles(board, board[2, 0])
                .Select(tile => tile.Coords);

            result.Should().HaveCount(3);
            result.Should().ContainInOrder((2, 1), (1, 0), (1, 1));
        }
    }
}