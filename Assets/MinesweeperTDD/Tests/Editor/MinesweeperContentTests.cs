using FluentAssertions;
using MinesweeperTDD.Runtime.Domain;
using MinesweeperTDD.Tests.TestDataBuilders;
using NUnit.Framework;

namespace MinesweeperTDD.Tests
{
    public class MinesweeperContentTests
    {
        [Test]
        public void HasFlag_IsFalse_ByDefault()
        {
            MinesweeperContent sut = MinesweeperContentBuilder.New();

            var result = sut.HasFlag;

            result.Should().Be(false);
        }
    }
}