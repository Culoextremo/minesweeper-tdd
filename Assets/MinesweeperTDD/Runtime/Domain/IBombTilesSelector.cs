
using Kalendra.BoardSystem.Runtime.Domain.Entities;
using Kalendra.Commons.Runtime.Architecture.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MinesweeperTDD.Tests
{
    public interface IBombTilesSelector
    {
        IEnumerable<(int, int)> GetBombTiles(IBoard board, int bombCount);
    }
    public class RandomBombTilesSelector : IBombTilesSelector
    {
        public RandomBombTilesSelector(IRandomService randomService)
        {

        }
        public IEnumerable<(int, int)> GetBombTiles(IBoard board, int bombCount)
        {
            int size = board.Size.x * board.Size.y;
            if (bombCount >= size)
            {
                bombCount = size - 1;
            }
            return board.ListAllEmptyTiles.Take(bombCount);
        }
    }
}
