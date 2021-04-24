using Kalendra.Commons.Runtime.Architecture.Services;
using Kalendra.Commons.Tests.TestDataBuilders.Services;

namespace Kalendra.Commons.Tests.TestDataBuilders.StaticShortcuts
{
    public static partial class Fake
    {
        public static RandomServiceMockBuilder RandomService() => RandomServiceMockBuilder.New();
    }
}