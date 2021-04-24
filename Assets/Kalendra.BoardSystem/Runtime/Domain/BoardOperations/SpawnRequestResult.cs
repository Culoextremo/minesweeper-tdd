namespace Kalendra.BoardSystem.Runtime.Domain.Entities.BoardOperations
{
    internal class SpawnRequestResult
    {
        public readonly (int x, int y) coordsWhereSpawn;
        public readonly ITileContent spawnedContent;

        public readonly ITileContent oldCachedContent;

        public SpawnRequestResult((int x, int y) coordsWhereSpawn, ITileContent spawnedContent, ITileContent oldCachedContent)
        {
            this.coordsWhereSpawn = coordsWhereSpawn;
            this.spawnedContent = spawnedContent;
            this.oldCachedContent = oldCachedContent;
        }

        public override string ToString()
        {
            return $"{nameof(coordsWhereSpawn)}: {coordsWhereSpawn}, {nameof(spawnedContent)}: {spawnedContent}, {nameof(oldCachedContent)}: {oldCachedContent}";
        }
    }
}