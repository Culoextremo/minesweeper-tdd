using System;
using System.Threading.Tasks;
using Kalendra.BoardSystem.Runtime.Domain.Policy;
using UnityEngine;

namespace Kalendra.BoardSystem.Runtime.Domain.Entities.BoardOperations
{
    public class MergeOperation : IBoardAsyncOperation
    {
        readonly ITile originTile;
        readonly ITile targetTile;

        readonly IMergeOperatorPolicy mergePolicy;

        MergeRequestResult mergeResultCache;

        #region Constructors
        public MergeOperation(ITile originTile, ITile targetTile, IMergeOperatorPolicy mergePolicy)
        {
            AssertDifferentTiles(originTile, targetTile);
            
            this.originTile = originTile;
            this.targetTile = targetTile;
            this.mergePolicy = mergePolicy;
        }

        void AssertDifferentTiles(ITile tile1, ITile tile2)
        {
            if(tile1 == tile2)
                throw new InvalidOperationException("Can't merge the same tile");
        }
        #endregion

        public bool IsAvailable(IBoard targetBoard)
        {
            return mergePolicy.IsAvailable(targetBoard);
        }

        public async Task Execute(IBoard targetBoard)
        {
            if(mergeResultCache is null)
                RequestMerge();
            
            targetTile.Content = mergeResultCache.mergedTargetContent;
            originTile.Content = new NullTileContent();
        }

        void RequestMerge()
        {
            var targetContent = mergePolicy.Merge(originTile.Content, targetTile.Content);
            SaveMergeResultInCache(targetContent);
        }

        void SaveMergeResultInCache(ITileContent mergedContent)
        {
            mergeResultCache = new MergeRequestResult(originTile.Content, targetTile.Content, mergedContent);
        }

        public async Task Undo(IBoard targetBoard)
        {
            originTile.Content = mergeResultCache.oldOriginTileContent;
            targetTile.Content = mergeResultCache.oldTargetTileContent;
        }
    }
}