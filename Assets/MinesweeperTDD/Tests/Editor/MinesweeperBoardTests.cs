using System;
using FluentAssertions;
using MinesweeperTDD.Runtime.Domain;
using MinesweeperTDD.Tests.TestDataBuilders;
using NUnit.Framework;


namespace MinesweeperTDD.Tests
{
    class MinesweeperBoardTests
    {
        [Test]
        public void Size_IsSet_ByConstructor()
        {
            MinesweeperBoard sut = MinesweeperBoardBuilder.New().WithSize(3, 4);

            var result = sut.Size;

            result.Should().Be((3, 4));
        }

        [Test]
        public void BombCount_NonPositive_ThrowsException()
        {
            var sut = MinesweeperBoardBuilder.New().WithBombCount(0);

            Action act = () => sut.Build();

            act.Should().ThrowExactly<ArgumentOutOfRangeException>();
        }
       
        [Test]
        public void BombCount_IsPlaced_ByConstructor()
        {
            MinesweeperBoard sut = MinesweeperBoardBuilder.New().WithSize(1,5).WithBombCount(4);

            sut.GetContent(0, 0).IsBomb.Should().BeTrue();
            sut.GetContent(0, 1).IsBomb.Should().BeTrue();
            sut.GetContent(0, 2).IsBomb.Should().BeTrue();
            sut.GetContent(0, 3).IsBomb.Should().BeTrue();
        }

        [Test, TestCase(1,1,2), TestCase(1,2,3)]
        public void Size_IsGreaterThan_BombCount(int x, int y, int bombCount)
        {
            var sut = MinesweeperBoardBuilder.New().WithSize(x, y).WithBombCount(bombCount);

            Action act = () => sut.Build();

            act.Should().ThrowExactly<ArgumentOutOfRangeException>();
        }

        [Test]
        public void EveryTile_Has_MinesweeperContent()
        {
            MinesweeperBoard sut = MinesweeperBoardBuilder.New().WithSize(1, 2);

            sut[0, 0].Content.Should().BeOfType<MinesweeperContent>();
            sut[0, 1].Content.Should().BeOfType<MinesweeperContent>();
        }
    }
}
