using System.Linq;
using FluentAssertions;
using Kalendra.BoardSystem.Runtime.Domain.AdjacencyPolicy;
using Kalendra.BoardSystem.Tests.TestDataBuilders.StaticShortcuts;
using NUnit.Framework;

namespace Kalendra.BoardSystem.Tests.Editor
{
    public class SimpleAdjacencyTests
    {
        #region Up
        [Test]
        public void Up_Null_ReturnsEmpty()
        {
            var board = Build.Board().Build();
            UpAdjacencyPolicy sut = new UpAdjacencyPolicy();

            var result = sut.GetAdjacentTiles(board, null);

            result.Should().BeEmpty();
        }

        [Test]
        public void Up_OutOfRange_ReturnsEmpty()
        {
            var board = Build.Board().Build();
            UpAdjacencyPolicy sut = new UpAdjacencyPolicy();

            var result = sut.GetAdjacentTiles(board, board[0, 0]);

            result.Should().BeEmpty();
        }

        [Test]
        public void Up_InRange_ReturnsTileAbove()
        {
            var board = Build.Board().WithSize(1, 2).Build();
            UpAdjacencyPolicy sut = new UpAdjacencyPolicy();

            var result = sut.GetAdjacentTiles(board, board[0, 0]);

            result.Single().Coords.Should().Be((0, 1));
        }
        #endregion
        
        #region Down
        [Test]
        public void Down_Null_ReturnsEmpty()
        {
            var board = Build.Board().Build();
            DownAdjacencyPolicy sut = new DownAdjacencyPolicy();

            var result = sut.GetAdjacentTiles(board, null);

            result.Should().BeEmpty();
        }

        [Test]
        public void Down_OutOfRange_ReturnsEmpty()
        {
            var board = Build.Board().Build();
            DownAdjacencyPolicy sut = new DownAdjacencyPolicy();

            var result = sut.GetAdjacentTiles(board, board[0, 0]);

            result.Should().BeEmpty();
        }

        [Test]
        public void Down_InRange_ReturnsTileBelow()
        {
            var board = Build.Board().WithSize(1, 2).Build();
            DownAdjacencyPolicy sut = new DownAdjacencyPolicy();

            var result = sut.GetAdjacentTiles(board, board[0, 1]);

            result.Single().Coords.Should().Be((0, 0));
        }
        #endregion
        
        #region Right
        [Test]
        public void RightAdjacency_NullTile_ReturnsEmpty()
        {
            var board = Build.Board().Build();
            RightAdjacencyPolicy sut = new RightAdjacencyPolicy();

            var result = sut.GetAdjacentTiles(board, null);

            result.Should().BeEmpty();
        }
        
        [Test]
        public void RightAdjacency_OutOfRange_ReturnsEmpty()
        {
            var board = Build.Board().Build();
            RightAdjacencyPolicy sut = new RightAdjacencyPolicy();

            var result = sut.GetAdjacentTiles(board, board[0, 0]);

            result.Should().BeEmpty();
        }

        [Test]
        public void RightAdjacency_InRange_ReturnsTileLeftwards()
        {
            var board = Build.Board().WithSize(2, 1).Build();
            RightAdjacencyPolicy sut = new RightAdjacencyPolicy();

            var result = sut.GetAdjacentTiles(board, board[0, 0]);

            result.Single().Coords.Should().Be((1, 0));
        }
        #endregion
        
        #region Left
        [Test]
        public void LeftAdjacency_NullTile_ReturnsEmpty()
        {
            var board = Build.Board().Build();
            LeftAdjacencyPolicy sut = new LeftAdjacencyPolicy();

            var result = sut.GetAdjacentTiles(board, null);

            result.Should().BeEmpty();
        }
        
        [Test]
        public void LeftAdjacency_OutOfRange_ReturnsEmpty()
        {
            var board = Build.Board().Build();
            LeftAdjacencyPolicy sut = new LeftAdjacencyPolicy();

            var result = sut.GetAdjacentTiles(board, board[0, 0]);

            result.Should().BeEmpty();
        }

        [Test]
        public void LeftAdjacency_InRange_ReturnsTileRightwards()
        {
            var board = Build.Board().WithSize(2, 1).Build();
            LeftAdjacencyPolicy sut = new LeftAdjacencyPolicy();

            var result = sut.GetAdjacentTiles(board, board[1, 0]);

            result.Single().Coords.Should().Be((0, 0));
        }
        #endregion
        
        #region UpRight
        [Test]
        public void UpRight_Null_ReturnsEmpty()
        {
            var board = Build.Board().Build();
            UpRightAdjacencyPolicy sut = new UpRightAdjacencyPolicy();

            var result = sut.GetAdjacentTiles(board, null);

            result.Should().BeEmpty();
        }

        [Test]
        public void UpRight_OutOfRange_ReturnsEmpty()
        {
            var board = Build.Board().Build();
            UpRightAdjacencyPolicy sut = new UpRightAdjacencyPolicy();

            var result = sut.GetAdjacentTiles(board, board[0, 0]);

            result.Should().BeEmpty();
        }

        [Test]
        public void UpRight_InRange_ReturnsTileAbove()
        {
            var board = Build.Board().WithSize(2, 2).Build();
            UpRightAdjacencyPolicy sut = new UpRightAdjacencyPolicy();

            var result = sut.GetAdjacentTiles(board, board[0, 0]);

            result.Single().Coords.Should().Be((1, 1));
        }
        #endregion
        
        #region RightDown
        [Test]
        public void RightDown_Null_ReturnsEmpty()
        {
            var board = Build.Board().Build();
            RightDownAdjacencyPolicy sut = new RightDownAdjacencyPolicy();

            var result = sut.GetAdjacentTiles(board, null);

            result.Should().BeEmpty();
        }

        [Test]
        public void RightDown_OutOfRange_ReturnsEmpty()
        {
            var board = Build.Board().Build();
            RightDownAdjacencyPolicy sut = new RightDownAdjacencyPolicy();

            var result = sut.GetAdjacentTiles(board, board[0, 0]);

            result.Should().BeEmpty();
        }

        [Test]
        public void RightDown_InRange_ReturnsTileAbove()
        {
            var board = Build.Board().WithSize(2, 2).Build();
            RightDownAdjacencyPolicy sut = new RightDownAdjacencyPolicy();

            var result = sut.GetAdjacentTiles(board, board[0, 1]);

            result.Single().Coords.Should().Be((1, 0));
        }
        #endregion
        
        #region LeftDown
        [Test]
        public void LeftDown_Null_ReturnsEmpty()
        {
            var board = Build.Board().Build();
            LeftDownAdjacencyPolicy sut = new LeftDownAdjacencyPolicy();

            var result = sut.GetAdjacentTiles(board, null);

            result.Should().BeEmpty();
        }

        [Test]
        public void LeftDown_OutOfRange_ReturnsEmpty()
        {
            var board = Build.Board().Build();
            LeftDownAdjacencyPolicy sut = new LeftDownAdjacencyPolicy();

            var result = sut.GetAdjacentTiles(board, board[0, 0]);

            result.Should().BeEmpty();
        }

        [Test]
        public void LeftDown_InRange_ReturnsTileAbove()
        {
            var board = Build.Board().WithSize(2, 2).Build();
            LeftDownAdjacencyPolicy sut = new LeftDownAdjacencyPolicy();

            var result = sut.GetAdjacentTiles(board, board[1, 1]);

            result.Single().Coords.Should().Be((0, 0));
        }
        #endregion

        #region UpLeft
        [Test]
        public void UpLeft_Null_ReturnsEmpty()
        {
            var board = Build.Board().Build();
            UpLeftAdjacencyPolicy sut = new UpLeftAdjacencyPolicy();

            var result = sut.GetAdjacentTiles(board, null);

            result.Should().BeEmpty();
        }

        [Test]
        public void UpLeft_OutOfRange_ReturnsEmpty()
        {
            var board = Build.Board().Build();
            UpLeftAdjacencyPolicy sut = new UpLeftAdjacencyPolicy();

            var result = sut.GetAdjacentTiles(board, board[0, 0]);

            result.Should().BeEmpty();
        }

        [Test]
        public void UpLeft_InRange_ReturnsTileAbove()
        {
            var board = Build.Board().WithSize(2, 2).Build();
            UpLeftAdjacencyPolicy sut = new UpLeftAdjacencyPolicy();

            var result = sut.GetAdjacentTiles(board, board[1, 0]);

            result.Single().Coords.Should().Be((0, 1));
        }
        #endregion
    }
}