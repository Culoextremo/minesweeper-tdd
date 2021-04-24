using Kalendra.Commons.Runtime.Architecture.Services;
using Kalendra.Commons.Tests.TestDataBuilders.Builders;
using NSubstitute;

namespace Kalendra.Commons.Tests.TestDataBuilders.Services
{
    public class RandomServiceMockBuilder : MockBuilder<IRandomService>
    {
        #region Fluent API
        public RandomServiceMockBuilder WithTossUps(bool returned)
        {
            mock.TossUp(default).ReturnsForAnyArgs(returned);
            mock.TossUpToBeat(default).ReturnsForAnyArgs(returned);
            mock.TossUpPercentage(default).ReturnsForAnyArgs(returned);
            
            return this;
        }
        #endregion
        
        #region ObjectMother/FactoryMethods
        RandomServiceMockBuilder() { }

        public static RandomServiceMockBuilder New() => new RandomServiceMockBuilder();
        #endregion
    }
}