using Kalendra.BoardSystem.Runtime.Domain.BoardQueries;
using Kalendra.Commons.Tests.TestDataBuilders.Builders;
using NSubstitute;

namespace Kalendra.BoardSystem.Tests.TestDataBuilders.Domain
{
    public class BoardQueryBuilder<T> : MockBuilder<IBoardQuery<T>>
    {
        T requestResult;
        #region Fluent API
        public BoardQueryBuilder<T> WithRequestReturns(T requestResult)
        {
            mock.Request(default).ReturnsForAnyArgs(requestResult);
            return this;
        }
        #endregion

        #region ObjectMother/FactoryMethods
        BoardQueryBuilder()
        {
        }

        public static BoardQueryBuilder<T> New() => new BoardQueryBuilder<T>();
        #endregion
    }
}