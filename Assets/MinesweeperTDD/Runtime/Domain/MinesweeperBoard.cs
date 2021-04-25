using Kalendra.BoardSystem.Runtime.Domain.Entities;
using MinesweeperTDD.Runtime.Domain;
using System;

namespace MinesweeperTDD.Tests
{
    public class MinesweeperBoard : Board
    {
        public MinesweeperContent GetContent(int x, int y) => tiles[(x, y)].Content as MinesweeperContent;

        public MinesweeperBoard(int sizeX, int sizeY, int bombCount, IBombTilesSelector selector) : base(sizeX, sizeY)
        {
            AssertBombCountIsNotNegative(bombCount);
            AssertBombCountIsSmallerThanSize(sizeX, sizeY, bombCount);

            InitBombs(bombCount, selector);
            InitNoBombTileContents();
        }

        void InitNoBombTileContents()
        {
            foreach(var tileCoords in ListAllEmptyTiles)
                tiles[tileCoords].Content = new MinesweeperContent(false);
        }

        void InitBombs(int bombsToSpawn, IBombTilesSelector selector)
        {
            foreach(var bombTile in selector.GetBombTiles(this, bombsToSpawn))
                tiles[bombTile].Content = new MinesweeperContent(true);
        }

        static void AssertBombCountIsNotNegative(int bombCount)
        {
            if(bombCount < 0)
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

        public void SwitchFlag(int x, int y)
        {
            var content = GetContent(x, y);
            content.HasFlag = !content.HasFlag;
        }
    }
}
