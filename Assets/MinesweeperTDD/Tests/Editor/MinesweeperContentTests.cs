using FluentAssertions;
using Kalendra.Minesweeper.Tests.TestDataBuilders.StaticShortcuts;
using MinesweeperTDD.Runtime.Domain;
using NUnit.Framework;

namespace MinesweeperTDD.Tests
{
    public class MinesweeperContentTests
    {
        [Test]
        public void HasFlag_IsFalse_ByDefault()
        {
            MinesweeperContent sut = Build.MinesweeperContent();

            var result = sut.HasFlag;

            result.Should().Be(false);
        }
    }
}