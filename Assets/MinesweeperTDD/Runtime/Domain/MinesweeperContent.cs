using Kalendra.BoardSystem.Runtime.Domain.Entities;

namespace MinesweeperTDD.Runtime.Domain
{
    public class MinesweeperContent : ITileContent
    {
        public MinesweeperContent(bool isBomb)
        {
            IsBomb = isBomb;
        }

        public bool IsBomb { get; }
        public bool HasFlag { get; set; }
    }
}