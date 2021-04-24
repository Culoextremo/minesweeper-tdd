using System.Collections;
using System.Collections.Generic;

namespace Kalendra.BoardSystem.Runtime.Domain.AdjacencyPolicy
{
    public class LShapedAdjacencyPolicy : GroupAdjacencyPolicyTemplate
    {
        protected override IEnumerable<(int x, int y)> GetAllAdjacentCoords((int x, int y) coords)
        {
            var (x, y) = coords;
            return new[]
            {
                (x - 1, y - 2),
                (x - 1, y + 2),
                (x + 1, y - 2),
                (x + 1, y + 2),
                (x - 2, y - 1),
                (x - 2, y + 1),
                (x + 2, y - 1),
                (x + 2, y + 1)
            };
        }
    }
}