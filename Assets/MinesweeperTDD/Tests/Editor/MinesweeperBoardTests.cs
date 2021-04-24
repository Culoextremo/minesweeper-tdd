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
            var sut = new MinesweeperBoard(3, 4);

            var result = sut.Size;

            result.Should().Be((3, 4));
        }
        [Test]
        public void EveryTile_Has_MinesweeperContent()
        {
            var sut = new MinesweeperBoard(1, 1);

            var result = sut.GetTile(0, 0).Content;

            result.Should().BeOfType<MinesweeperContent>();
        }
    }
}
