using System;
using System.Linq;
using FluentAssertions;
using Kalendra.BoardSystem.Runtime.Domain.Entities;
using Kalendra.BoardSystem.Tests.TestDataBuilders.StaticShortcuts;
using NUnit.Framework;

namespace Kalendra.BoardSystem.Tests.Editor
{
    public class BoardTests
    {
        #region New
        [Test]
        public void New_IsCreatedWithSize()
        {
            var (expectedSizeX, expectedSizeY) = (2, 3);
            Board sut = Build.Board().WithSize(expectedSizeX, expectedSizeY);

            var resultSize = sut.Size;
            
            resultSize.Should().BeEquivalentTo((expectedSizeX, expectedSizeY));
        }

        [Test]
        public void New_WithSize_NonPositive_ThrowsException()
        {
            Board sut;
            
            Action actFirstArg = () => sut = Build.Board().WithSize(-1, 2);
            Action actSecondArg = () => sut = Build.Board().WithSize(2, 0);

            actFirstArg.Should().Throw<ArgumentOutOfRangeException>();
            actSecondArg.Should().Throw<ArgumentOutOfRangeException>();
        }
        #endregion

        #region GetTile
        [Test]
        public void GetTile_TileIsNotNull_ByDefault()
        {
            Board sut = Build.Board();

            var result = sut[0, 0];

            result.Should().NotBeNull();
        }
        
        [Test]
        public void GetTile_OutOfRange_ReturnsNullTile()
        {
            Board sut = Build.Board();

            var result = sut[1, 0];

            result.Should().BeOfType<NullTile>();
        }

        [Test]
        public void GetTile_SameThan_Indexer()
        {
            IBoard sut = Build.Board().Build();

            var expected = sut[0, 0];
            var result = sut[0, 0];
            
            result.Should().BeEquivalentTo(expected);
        }
        #endregion
        
        #region ListAllEmptyTiles
        [Test]
        public void ListAllEmptyTiles_ReturnsAllTiles_ByDefault()
        {
            Board sut = Build.Board();

            var resultEmptyTiles = sut.ListAllEmptyTiles.ToList();

            resultEmptyTiles.Should().Contain((0, 0));
            resultEmptyTiles.Should().HaveCount(1);
        }

        [Test]
        public void ListAllEmptyTiles_WhenNoEmptyTiles_ReturnsEmptyCollection()
        {
            Board sut = Build.Board();
            sut[0, 0].Content = Fake.TileContent_NotNull();
            
            var resultemptyTiles = sut.ListAllEmptyTiles.ToList();
            
            resultemptyTiles.Should().BeEmpty();
        }

        [Test]
        public void ListAllEmptyTiles_WhenNoTilesInBoard_ReturnsEmptyCollection()
        {
            Board sut = Build.Board_WithNoTiles();
            
            var resultemptyTiles = sut.ListAllEmptyTiles.ToList();
            
            resultemptyTiles.Should().BeEmpty();
        }
        #endregion

        #region HasTile
        [Test]
        public void HasTile_InRange_ReturnsTrue()
        {
            Board sut = Build.Board();

            var result = sut.HasTile(0, 0);

            result.Should().BeTrue();
        }

        [Test]
        public void HasTile_NegativeRange_ReturnsFalse()
        {
            Board sut = Build.Board();

            var resultX = sut.HasTile(-1, 0);
            var resultY = sut.HasTile(0, -1);

            resultX.Should().BeFalse();
            resultY.Should().BeFalse();
        }

        [Test]
        public void HasTile_OutOfRange_ReturnsFalse()
        {
            Board sut = Build.Board();

            var resultX = sut.HasTile(0, 1);
            var resultY = sut.HasTile(1, 0);

            resultX.Should().BeFalse();
            resultY.Should().BeFalse();
        }
        #endregion

        #region RemoveTile
        [Test]
        public void RemoveTile_ReturnsTrue_ByDefault()
        {
            Board sut = Build.Board();

            var resultRemoved = sut.RemoveTile(0, 0);

            resultRemoved.Should().BeTrue();
        }

        [Test]
        public void RemoveTile_OutOfRange_ReturnsFalse()
        {
            Board sut = Build.Board();

            var resultRemoved = sut.RemoveTile(1, 0);

            resultRemoved.Should().BeFalse();
        }

        [Test]
        public void RemoveTile_OnPreviouslyRemovedTile_ReturnsFalse()
        {
            Board sut = Build.Board();

            sut.RemoveTile(0, 0);
            var resultRemoved = sut.RemoveTile(0, 0);

            resultRemoved.Should().BeFalse();
        }
        
        [Test]
        public void RemoveTile_ThenHasTile_ReturnsFalse()
        {
            Board sut = Build.Board();

            var resultBefore = sut.HasTile(0, 0);
            sut.RemoveTile(0, 0);
            var resultAfter = sut.HasTile(0, 0);

            resultBefore.Should().BeTrue();
            resultAfter.Should().BeFalse();
        }

        [Test]
        public void RemoveTile_WhenRemovesACompleteLine_ThenSizeIsUpdated()
        {
            Board sut = Build.Board();

            var resultBefore = sut.Size;
            sut.RemoveTile(0, 0);
            var resultAfter = sut.Size;

            resultBefore.Should().Be((1, 1));
            resultAfter.Should().Be((0, 0));
        }
        #endregion

        #region AddTile
        [Test]
        public void AddTile_OnExistingTile_ReturnsFalse()
        {
            Board sut = Build.Board();

            var resultAdded = sut.AddTile(0, 0);

            resultAdded.Should().BeFalse();
        }

        [Test]
        public void AddTile_NegativeCoords_ThenHasTile_ReturnsTrue()
        {
            Board sut = Build.Board();

            var resultBefore = sut.HasTile(-1, -1);
            sut.AddTile(-1, -1);
            var resultAfter = sut.HasTile(-1, -1);

            resultBefore.Should().BeFalse();
            resultAfter.Should().BeTrue();
        }

        [Test, Category("TODO")]
        public void AddTile_OnNewRange_ThenOthersInRange_AreNotAdded()
        {
            
        }
        
        [Test]
        public void AddTile_OutOfRange_ThenHasTile_ReturnsTrue()
        {
            Board sut = Build.Board_WithNoTiles();

            var resultBefore = sut.HasTile(0, 0);
            sut.AddTile(0, 0);
            var resultAfter = sut.HasTile(0, 0);

            resultBefore.Should().BeFalse();
            resultAfter.Should().BeTrue();
        }

        [Test]
        public void AddTile_WhenNewLineIsCompleted_ThenSizeIsUpdated()
        {
            Board sut = Build.Board_WithNoTiles();

            var resultBefore = sut.Size;
            sut.AddTile(0, 0);
            var resultAfter = sut.Size;

            resultBefore.Should().Be((0, 0));
            resultAfter.Should().Be((1, 1));
        }
        #endregion
    }
}