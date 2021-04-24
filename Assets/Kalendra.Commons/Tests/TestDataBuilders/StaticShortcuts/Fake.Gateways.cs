using Kalendra.BoardSystem.Tests.TestDataBuilders.Domain;
using Kalendra.Commons.Runtime.Architecture.Gateways;

namespace Kalendra.Commons.Tests.TestDataBuilders.StaticShortcuts
{
    public static partial class Fake
    {
        public static IReadOnlyAsyncRepository<T> ReadOnlyRepository<T>() where T : new() => ReadOnlyRepositoryMockBuilder<T>.New().ReturnsDefaults();
    }
}