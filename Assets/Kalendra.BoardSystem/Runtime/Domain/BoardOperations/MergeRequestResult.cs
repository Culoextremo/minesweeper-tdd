namespace Kalendra.BoardSystem.Runtime.Domain.Entities.BoardOperations
{
    internal class MergeRequestResult
    {
        public readonly ITileContent oldTargetTileContent;
        public readonly ITileContent oldOriginTileContent;

        public readonly ITileContent mergedTargetContent;

        public MergeRequestResult(ITileContent oldOriginTileContent, ITileContent oldTargetTileContent, ITileContent mergedTargetContent)
        {
            this.oldOriginTileContent = oldOriginTileContent;
            this.oldTargetTileContent = oldTargetTileContent;

            this.mergedTargetContent = mergedTargetContent;
        }
    }
}