using System.Threading.Tasks;
using JetBrains.Annotations;
using Kalendra.BoardSystem.Runtime.Domain.Entities;

namespace Kalendra.BoardSystem.Runtime.Domain.Policy
{
    public interface ISpawnOperatorPolicy
    {
        ITile SelectTileWhereSpawn(IBoard board);
        Task<ITileContent> SpawnContent();
    }
    
    public interface IMergeOperatorPolicy
    {
        bool IsAvailable(IBoard board);
        ITileContent Merge([NotNull] params ITileContent[] coloredPieces);
    }
}