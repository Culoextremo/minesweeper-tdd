namespace Kalendra.BoardSystem.Runtime.Domain.Entities
{
    public interface ITile
    {
        (int x, int y) Coords { get; }
        
        bool IsEmpty { get; }
        ITileContent Content { get; set; }
    }

    public class NullTile : ITile
    {
        public (int x, int y) Coords { get; }

        public bool IsEmpty => true;

        public ITileContent Content
        {
            get => new NullTileContent();
            set { } //Empty to accomplish Liskov's Substitution Principle. 
        }
    }
}