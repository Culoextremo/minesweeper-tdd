using Kalendra.BoardSystem.Runtime.Domain.Entities;
using MinesweeperTDD.Runtime.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinesweeperTDD.Tests
{
    public class MinesweeperBoard : Board
    {
        int bombCount; 
        
        public MinesweeperBoard(int sizeX, int sizeY, int bombCount) : base(sizeX, sizeY)
        {
            AssertBombCountIsPositive(bombCount);
            
            this.bombCount = bombCount;
            
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
            tiles[(0, 0)].Content = new MinesweeperContent(true);
        }

        static void AssertBombCountIsPositive(int bombCount)
        {
            if(bombCount <= 0)
                throw new ArgumentOutOfRangeException(nameof(bombCount));
        }
    }
}
