using System.Collections.Generic;

namespace Kalendra.BoardSystem.Runtime.Domain.Entities.BoardOperations
{
    public class BoardOperationResult
    {
        public List<IBoardAsyncOperation> CollateralOperations { get; }
    }
}