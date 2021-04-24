using Kalendra.BoardSystem.Runtime.Domain.Entities;
using MinesweeperTDD.Runtime.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kalendra.Commons.Runtime.Architecture.Services;

namespace MinesweeperTDD.Tests
{
    public class MinesweeperBoard : Board
    {
        public MinesweeperContent GetContent(int x, int y) => tiles[(x, y)].Content as MinesweeperContent;

        public MinesweeperBoard(int sizeX, int sizeY, int bombCount) : base(sizeX, sizeY)
        {
            AssertBombCountIsPositive(bombCount);
            AssertBombCountIsSmallerThanSize(sizeX, sizeY, bombCount);

            InitTileContents(sizeX, sizeY);
            InitBombs(bombCount);
        }

        void InitTileContents(int sizeX, int sizeY)
        {
            for(var i = 0; i < sizeX; i++)
            for(var j = 0; j < sizeY; j++)
                tiles[(i, j)].Content = new MinesweeperContent(false);
        }

        void InitBombs(int bombsToSpawn)
        {
            var values = tiles.Values.ToArray();
            for (int i = 0; i < bombsToSpawn; i++)
            {
                values[i].Content = new MinesweeperContent(true);
            }
        }

        static void AssertBombCountIsPositive(int bombCount)
        {
            if(bombCount <= 0)
                throw new ArgumentOutOfRangeException(nameof(bombCount));
        }
        static void AssertBombCountIsSmallerThanSize(int sizeX, int sizeY, int bombCount)
        {
            int size = sizeX * sizeY;
            if (bombCount >= size)
            {
                throw new ArgumentOutOfRangeException(nameof(bombCount));
            }
        }
    }
}
