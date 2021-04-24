using System.Collections.Generic;

namespace Kalendra.BoardSystem.Runtime.Domain.AdjacencyPolicy
{
    public class SquareAdjacencyPolicy : CompositeAdjacencyPolicyTemplate
    {
        protected override IEnumerable<IAdjacencyPolicy> OrderedPoliciesTemplateMethod()
        {
            return new IAdjacencyPolicy[]
            {
                new UpAdjacencyPolicy(),
                new UpRightAdjacencyPolicy(),
                new RightAdjacencyPolicy(),
                new RightDownAdjacencyPolicy(),
                new DownAdjacencyPolicy(),
                new LeftDownAdjacencyPolicy(),
                new LeftAdjacencyPolicy(),
                new UpLeftAdjacencyPolicy()
            };
        }
    }
}