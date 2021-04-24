using System.Collections.Generic;
using Kalendra.BoardSystem.Runtime.Domain.Entities.BoardOperations;
using Kalendra.Commons.Tests.TestDataBuilders.Builders;

namespace Kalendra.BoardSystem.Tests.TestDataBuilders.Domain
{
    internal class BoardOperationsManagerBuilder : Builder<BoardAsyncOperationScheduler>
    {
        List<IBoardAsyncOperation> begginingOperations;

        #region Fluent API
        public BoardOperationsManagerBuilder WithOperations(params IBoardAsyncOperation[] begginingOperations)
        {
            this.begginingOperations = new List<IBoardAsyncOperation>(begginingOperations);
            return this;
        }
        #endregion

        #region ObjectMother/FactoryMethods
        public static BoardOperationsManagerBuilder New() => new BoardOperationsManagerBuilder();
        #endregion

        #region Builder implementation
        public override BoardAsyncOperationScheduler Build() => new BoardAsyncOperationScheduler(begginingOperations);
        #endregion
    }
}