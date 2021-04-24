using Kalendra.BoardSystem.Runtime.Domain.Entities;
using Kalendra.Commons.Tests.TestDataBuilders.Builders;

namespace Kalendra.BoardSystem.Tests.TestDataBuilders.Domain
{
    internal class TileContentMockBuilder : MockBuilder<ITileContent>
    {
        TileContentMockBuilder() { }
        
        #region ObjectMother/FactoryMethods
        public static TileContentMockBuilder New() => new TileContentMockBuilder();
        #endregion
    }
}