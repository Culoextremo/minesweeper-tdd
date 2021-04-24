using System.Collections.Generic;

namespace Kalendra.BoardSystem.Runtime.Domain.Entities
{
    public interface IBoard
    {
        (int x, int y) Size { get; }

        bool HasTile(int i, int j);
        ITile GetTile(int i, int j);
        ITile this[int i, int j] { get; }
        
        bool RemoveTile(int i, int j);
        bool AddTile(int i, int j);
        
        IEnumerable<(int x, int y)> ListAllEmptyTiles { get; }
    }
}