using MinesweeperTDD.Tests.TestDataBuilders;

namespace Kalendra.Minesweeper.Tests.TestDataBuilders.StaticShortcuts
{
    public static partial class Build
    {
        public static MinesweeperBoardBuilder MinesweeperBoard() => MinesweeperBoardBuilder.New();
        public static MinesweeperContentBuilder MinesweeperContent() => MinesweeperContentBuilder.New(); 
    }
}