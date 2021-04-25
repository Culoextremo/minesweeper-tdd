using System;
using FluentAssertions;
using Kalendra.Minesweeper.Tests.TestDataBuilders.StaticShortcuts;
using MinesweeperTDD.Runtime.Domain;
using NSubstitute;
using NUnit.Framework;


namespace MinesweeperTDD.Tests
{
    class MinesweeperBoardTests
    {
        #region Construction
        [Test]
        public void Size_IsSet_ByConstructor()
        {
            MinesweeperBoard sut = Build.MinesweeperBoard().WithSize(3, 4);

            var result = sut.Size;

            result.Should().Be((3, 4));
        }

        [Test]
        public void BombCount_IsNegative_ThrowsException()
        {
            var sut = Build.MinesweeperBoard().WithBombCount(-1);

            Action act = () => sut.Build();

            act.Should().ThrowExactly<ArgumentOutOfRangeException>();
        }
       
        [Test]
        public void BombCount_IsPlaced_ByConstructor()
        {
            MinesweeperBoard sut = Build.MinesweeperBoard().WithSize(1,2).WithBombCount(1);

            var anyBomb = sut.GetContent(0, 0).IsBomb || sut.GetContent(0, 1).IsBomb;

            anyBomb.Should().BeTrue();
        }

        [Test, TestCase(1,1,2), TestCase(1,2,3)]
        public void When_BombCountIsGreaterThanSize_ThrowsException(int x, int y, int bombCount)
        {
            var sut = Build.MinesweeperBoard().WithSize(x, y).WithBombCount(bombCount);

            Action act = () => sut.Build();

            act.Should().ThrowExactly<ArgumentOutOfRangeException>();
        }

        [Test]
        public void EveryTile_Has_MinesweeperContent()
        {
            MinesweeperBoard sut = Build.MinesweeperBoard().WithSize(1, 2);

            sut[0, 0].Content.Should().BeOfType<MinesweeperContent>();
            sut[0, 1].Content.Should().BeOfType<MinesweeperContent>();
        }
        
        [Test]
        public void Bombs_ArePlaced_BySelector()
        {
            //Arrange
            var selectorMock = Substitute.For<IBombTilesSelector>();
            selectorMock.GetBombTiles(default, default).ReturnsForAnyArgs(new[] {(0, 1)});
            
            MinesweeperBoard sut = Build.MinesweeperBoard().WithSize(1, 2).WithSelector(selectorMock);
            
            //Act
            var result = sut.GetContent(0, 1);

            //Assert
            result.IsBomb.Should().BeTrue();
        }
        #endregion
        
        #region Flags
        [Test]
        public void IsFlag_OnNewBoard_IsFalse_ByDefault()
        {
            MinesweeperBoard sut = Build.MinesweeperBoard();

            var result = sut.GetContent(0, 0).HasFlag; 
            
            result.Should().BeFalse();
        }
        
        [Test]
        public void SwitchFlag_OnNewBoard_ThenFlagIsOn_InThatTile()
        {
            MinesweeperBoard sut = Build.MinesweeperBoard();

            sut.SwitchFlag(0, 0);

            sut.GetContent(0, 0).HasFlag.Should().BeTrue();
        }
        
        [Test]
        public void SwitchFlag_WhereFlagIsOn_ThenFlagIsOff()
        {
            MinesweeperBoard sut = Build.MinesweeperBoard();
            sut.SwitchFlag(0, 0);

            sut.SwitchFlag(0, 0);

            sut.GetContent(0, 0).HasFlag.Should().BeFalse();
        }
        #endregion
    }
}
