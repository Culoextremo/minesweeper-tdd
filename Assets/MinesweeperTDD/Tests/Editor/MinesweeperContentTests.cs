using FluentAssertions;
using Kalendra.Commons.Tests.TestDataBuilders.Builders;
using MinesweeperTDD.Runtime.Domain;
using MinesweeperTDD.Tests.TestDataBuilders;
using NUnit.Framework;

namespace UnityEngine.TestTools.Utils
{
    public class MinesweeperContentTests
    {
        [Theory]
        public void IsBomb_PropertySet(bool isBomb)
        {
            MinesweeperContent sut = MinesweeperContentBuilder.New().WithIsBomb(isBomb);

            var result = sut.IsBomb;

            result.Should().Be(isBomb);
        }
    }
}