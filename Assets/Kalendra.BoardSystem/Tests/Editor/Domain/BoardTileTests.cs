using System;
using FluentAssertions;
using Kalendra.BoardSystem.Runtime.Domain.Entities;
using Kalendra.BoardSystem.Tests.TestDataBuilders.StaticShortcuts;
using NUnit.Framework;

namespace Kalendra.BoardSystem.Tests.Editor
{
    public class BoardTileTests
    {
        [Test]
        public void Coords_AreImmutable_SinceConstructor()
        {
            BoardTile sut = Build.BoardTile().WithCoords(2, 3);

            var result = sut.Coords;

            result.Should().BeEquivalentTo((2, 3));
        }

        [Test]
        public void Content_IsNullContent_ByDefault()
        {
            BoardTile sut = Build.BoardTile();

            var result = sut.Content;

            result.Should().BeOfType<NullTileContent>();
        }
    }
}