using System;
using FluentAssertions;
using Kalendra.BoardSystem.Runtime.Domain.Entities;
using MinesweeperTDD.Runtime.Domain;
using NUnit.Framework;


namespace MinesweeperTDD.Tests
{
    class MinesweeperBoardTests
    {
        [Test]
        public void Size_IsSet_ByConstructor()
        {
            var sut = new MinesweeperBoard(3, 4, 1);

            var result = sut.Size;

            result.Should().Be((3, 4));
        }

        [Test]
        public void BombCount_NonPositive_ThrowsException()
        {
            Action act = () =>  new MinesweeperBoard(1, 1, 0);

            act.Should().ThrowExactly<ArgumentOutOfRangeException>();
        }
        
        [Test]
        public void BombCount_IsSet_ByConstructor()
        {
            var sut = new MinesweeperBoard(1, 1, 1);
            
            var result = sut[0, 0].Content as MinesweeperContent;
            
            result.IsBomb.Should().BeTrue();
        }
        
        [Test]
        public void EveryTile_Has_MinesweeperContent()
        {
            var sut = new MinesweeperBoard(1, 1, 1);

            var result = sut[0, 0].Content;

            result.Should().BeOfType<MinesweeperContent>();
        }
    }
}
