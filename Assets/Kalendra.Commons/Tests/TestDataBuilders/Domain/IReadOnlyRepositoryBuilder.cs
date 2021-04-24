using System.Collections.Generic;
using System.Threading.Tasks;
using Kalendra.Commons.Runtime.Architecture.Gateways;
using Kalendra.Commons.Tests.TestDataBuilders.Builders;
using NSubstitute;

namespace Kalendra.BoardSystem.Tests.TestDataBuilders.Domain
{
    internal class ReadOnlyRepositoryMockBuilder<T> : MockBuilder<IReadOnlyAsyncRepository<T>> where T : new()
    {
        ReadOnlyRepositoryMockBuilder() { }

        #region Fluent API
        #endregion

        #region ObjectMother/FactoryMethods
        public static ReadOnlyRepositoryMockBuilder<T> New() => new ReadOnlyRepositoryMockBuilder<T>();

        public IReadOnlyAsyncRepository<T> ReturnsDefaults()
        {
            mock.Load(default).ReturnsForAnyArgs(Task.FromResult(new T()));
            mock.LoadAll().ReturnsForAnyArgs(Task.FromResult(new List<T> {new T()} as IEnumerable<T>));

            return Build();
        }
        #endregion
    }
}