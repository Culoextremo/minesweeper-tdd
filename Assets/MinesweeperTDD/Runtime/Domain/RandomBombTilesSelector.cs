
using Kalendra.BoardSystem.Runtime.Domain.Entities;
using Kalendra.Commons.Runtime.Architecture.Services;
using System.Collections.Generic;
using System.Linq;

namespace MinesweeperTDD.Tests
{
    public class RandomBombTilesSelector : IBombTilesSelector
    {
        private IRandomService randomService;
        public RandomBombTilesSelector(IRandomService randomService)
        {
            this.randomService = randomService;
        }
        public IEnumerable<(int, int)> GetBombTiles(IBoard board, int bombCount)
        {
            int size = board.Size.x * board.Size.y;
            if (bombCount >= size)
            {
                bombCount = size - 1;
            }
            var allEmptyTiles = board.ListAllEmptyTiles.ToList();
            var bombTiles = new List<(int, int)>();

            for (int i = 0; i < bombCount; i++)
            {
                var bombTile = randomService.GetRandom(allEmptyTiles);
                bombTiles.Add(bombTile);
                allEmptyTiles.Remove(bombTile);
            }
            return bombTiles;
        }
    }
}
