using Kalendra.BoardSystem.Runtime.Domain.Entities;
using Kalendra.Commons.Tests.TestDataBuilders.Builders;

namespace Kalendra.BoardSystem.Tests.TestDataBuilders.Domain
{
    internal class TileMockBuilder : MockBuilder<ITile>
    {
        TileMockBuilder() { }

        public static TileMockBuilder New() => new TileMockBuilder();
    }
}