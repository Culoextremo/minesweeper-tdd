using Kalendra.BoardSystem.Tests.TestDataBuilders.Domain;

namespace Kalendra.BoardSystem.Tests.TestDataBuilders.StaticShortcuts
{
    public static partial class Fake
    {
        public static BoardQueryBuilder<T> BoardQuery<T>() => BoardQueryBuilder<T>.New();
    }
}