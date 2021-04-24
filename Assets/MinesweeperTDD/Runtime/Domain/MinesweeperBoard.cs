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
        public MinesweeperBoard(int sizeX, int sizeY) : base(sizeX, sizeY)
        {
            for (var i = 0; i < sizeX; i++)
                for (var j = 0; j < sizeY; j++)
                    tiles[(i, j)].Content = new MinesweeperContent(false);
        }
    }
}
