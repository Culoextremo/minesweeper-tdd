using System.Collections.Generic;
using System.Linq;
using Kalendra.BoardSystem.Runtime.Domain.Entities;

namespace Kalendra.BoardSystem.Runtime.Domain.AdjacencyPolicy
{
    public abstract class CompositeAdjacencyPolicyTemplate : IAdjacencyPolicy
    {
        readonly IEnumerable<IAdjacencyPolicy> orderedPolicies;

        protected CompositeAdjacencyPolicyTemplate() => orderedPolicies = OrderedPoliciesTemplateMethod();

        #region Template Method Pattern
        protected abstract IEnumerable<IAdjacencyPolicy> OrderedPoliciesTemplateMethod();
        #endregion

        #region IAdjacencyPolicy implementation
        public IEnumerable<ITile> GetAdjacentTiles(IBoard board, ITile tile)
        {
            var orderedTiles = Enumerable.Empty<ITile>();
            orderedTiles = orderedPolicies.Aggregate(orderedTiles, (current, subPolicy) => current.Concat(subPolicy.GetAdjacentTiles(board, tile)));

            return orderedTiles;
        }
        #endregion
    }
}