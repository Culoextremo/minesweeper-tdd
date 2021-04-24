using System;
using FluentAssertions;
using Kalendra.BoardSystem.Runtime.Domain.Entities.BoardOperations;
using Kalendra.BoardSystem.Tests.TestDataBuilders.StaticShortcuts;
using NUnit.Framework;

namespace Kalendra.BoardSystem.Tests.Editor
{
    public class AddTileOperationTests
    {
        [Test]
        public void New_ReceivesCoords()
        {
            var sut = new AddTileBoardOperation(1, 1);

            var result = sut.Coords;

            result.Should().Be((1, 1));
        }

        [Test]
        public void IsAvailable_WhenBoardHasNotTheTile_IsTrue()
        {
            var noTiledBoard = Build.Board_WithNoTiles();
            var sut = new AddTileBoardOperation(0, 0);

            var result = sut.IsAvailable(noTiledBoard);
            result.Should().BeTrue();
        }
        
        [Test]
        public void IsAvailable_WhenBoardAlreadyHasTheTile_IsFalse()
        {
            var oneTiledBoard = Build.Board().Build();
            var sut = new AddTileBoardOperation(0, 0);

            var result = sut.IsAvailable(oneTiledBoard);
            result.Should().BeFalse();
        }

        [Test]
        public void Execute_OnBoardHavingAlreadyThatTile_ThrowsException()
        {
            var oneTiledBoard = Build.Board().Build();
            var sut = new AddTileBoardOperation(0, 0);

            Action act = () => sut.Execute(oneTiledBoard);

            act.Should().Throw<InvalidOperationException>();
        }

        [Test]
        public void Execute_OnNotExistingTile_ThenAddsTheTile()
        {
            var noTiledBoard = Build.Board_WithNoTiles();
            var sut = new AddTileBoardOperation(0, 0);

            sut.Execute(noTiledBoard);

            noTiledBoard.HasTile(0, 0).Should().BeTrue();
        }

        [Test]
        public void Undo_OnSameTileWasAdded_ThenRemovesTheTile()
        {
            var noTiledBoard = Build.Board_WithNoTiles();
            var sut = new AddTileBoardOperation(0, 0);

            sut.Execute(noTiledBoard);
            sut.Undo(noTiledBoard);

            noTiledBoard.HasTile(0, 0).Should().BeFalse();
        }
    }
}