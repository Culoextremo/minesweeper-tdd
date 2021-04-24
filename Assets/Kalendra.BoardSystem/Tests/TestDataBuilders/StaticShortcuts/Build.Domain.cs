using Kalendra.BoardSystem.Runtime.Domain.Entities;
using Kalendra.BoardSystem.Tests.TestDataBuilders.Domain;

namespace Kalendra.BoardSystem.Tests.TestDataBuilders.StaticShortcuts
{
    internal static partial class Build
    {
        internal static BoardBuilder Board() => BoardBuilder.New();
        internal static Board Board_WithNoTiles() => BoardBuilder.BoardWithNoTiles();
        
        internal static BoardTileBuilder BoardTile() => BoardTileBuilder.New();

        internal static SpawnOperationBuilder SpawnOperation() => SpawnOperationBuilder.New();
        internal static MergeOperationBuilder MergeOperation() => MergeOperationBuilder.New();

        internal static BoardOperationsManagerBuilder BoardOperationsManager() => BoardOperationsManagerBuilder.New();
    }
}