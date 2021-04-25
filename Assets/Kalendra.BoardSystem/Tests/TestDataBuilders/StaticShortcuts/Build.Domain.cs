using Kalendra.BoardSystem.Runtime.Domain.Entities;
using Kalendra.BoardSystem.Tests.TestDataBuilders.Domain;

namespace Kalendra.BoardSystem.Tests.TestDataBuilders.StaticShortcuts
{
    public static partial class Build
    {
        public static BoardBuilder Board() => BoardBuilder.New();
        public static Board Board_WithNoTiles() => BoardBuilder.BoardWithNoTiles();

        public static BoardTileBuilder BoardTile() => BoardTileBuilder.New();

        public static SpawnOperationBuilder SpawnOperation() => SpawnOperationBuilder.New();
        public static MergeOperationBuilder MergeOperation() => MergeOperationBuilder.New();

        public static BoardOperationsManagerBuilder BoardOperationsManager() => BoardOperationsManagerBuilder.New();
    }
}