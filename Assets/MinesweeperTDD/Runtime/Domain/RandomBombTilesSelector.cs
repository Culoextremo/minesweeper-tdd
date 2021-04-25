using Kalendra.BoardSystem.Runtime.Domain.Entities;
using Kalendra.Commons.Runtime.Architecture.Services;
using System.Collections.Generic;
using System.Linq;

namespace MinesweeperTDD.Tests
{
    public class RandomBombTilesSelector : IBombTilesSelector
    {
        private readonly IRandomService randomService;
        public RandomBombTilesSelector(IRandomService randomService)
        {
            this.randomService = randomService;
        }
        
        public IEnumerable<(int, int)> GetBombTiles(IBoard board, int bombCount)
        {
            bombCount = ClampBombCountInBoardBounds(board.Size, bombCount);
            
            var allEmptyTiles = board.ListAllEmptyTiles.ToList();
            var bombTiles = GetRandomTiles(allEmptyTiles, bombCount);
            
            return bombTiles;
        }

        #region Support methods
        int ClampBombCountInBoardBounds((int x, int y) boardSize, int bombCount)
        {
            int size = boardSize.x * boardSize.y;
            
            if (bombCount >= size)
                bombCount = size - 1;
            
            return bombCount;
        }
        
        IEnumerable<(int, int)> GetRandomTiles(ICollection<(int x, int y)> emptyTiles, int bombCount)
        {
            var bombTiles = new List<(int, int)>();

            for (int i = 0; i < bombCount; i++)
            {
                var bombTile = randomService.GetRandom(emptyTiles);
                bombTiles.Add(bombTile);
                emptyTiles.Remove(bombTile);
            }

            return bombTiles;
        }
        #endregion
    }
}
