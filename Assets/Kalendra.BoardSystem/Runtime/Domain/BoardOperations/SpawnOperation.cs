using System;
using System.Linq;
using System.Threading.Tasks;
using Kalendra.BoardSystem.Runtime.Domain.Policy;
using UnityEngine;

namespace Kalendra.BoardSystem.Runtime.Domain.Entities.BoardOperations
{
    public class SpawnOperation : IBoardAsyncOperation //TODO: <OperationRequestResult>
    {
        readonly ISpawnOperatorPolicy spawnPolicy;

        SpawnRequestResult spawnResultCache;

        public (int x, int y) CoordsWhereSpawn => spawnResultCache.coordsWhereSpawn;
        public ITileContent SpawnedContent => spawnResultCache.spawnedContent;

        public SpawnOperation(ISpawnOperatorPolicy spawnPolicy)
        {
            this.spawnPolicy = spawnPolicy;
        }

        #region IBoardOperation implementation
        public bool IsAvailable(IBoard targetBoard)
        {
            return targetBoard.ListAllEmptyTiles.Any(); //TODO: policy-dependent
        }

        public async Task Execute(IBoard targetBoard)
        {
            if(!IsAvailable(targetBoard))
                await Task.FromException(new InvalidOperationException());

            if(spawnResultCache is null)
                await RequestSpawn(targetBoard);

            SpawnCachedResult(targetBoard);
        }

        public async Task Undo(IBoard targetBoard)
        {
            if(spawnResultCache is null)
                await Task.FromException(new InvalidOperationException());

            var (x, y) = spawnResultCache.coordsWhereSpawn;
            if(spawnResultCache.spawnedContent != targetBoard.GetTile(x, y).Content)
                await Task.FromException(new ArgumentException());

            targetBoard.GetTile(x, y).Content = spawnResultCache.oldCachedContent;
        }
        #endregion
        
        #region Support methods
        async Task RequestSpawn(IBoard targetBoard)
        {
            var coords = spawnPolicy.SelectTileWhereSpawn(targetBoard).Coords;
            var spawnedContent = await spawnPolicy.SpawnContent();

            SaveSpawnResultInCache(targetBoard, coords, spawnedContent);
        }
        
        void SaveSpawnResultInCache(IBoard targetBoard, (int x, int y) coords, ITileContent spawnedContent)
        {
            var cachedContent = targetBoard.GetTile(coords.x, coords.y).Content;
            spawnResultCache = new SpawnRequestResult(coords, spawnedContent, cachedContent);
        }

        void SpawnCachedResult(IBoard targetBoard)
        {
            var (x, y) = spawnResultCache.coordsWhereSpawn;
            targetBoard.GetTile(x, y).Content = spawnResultCache.spawnedContent;
        }
        #endregion
    }
}