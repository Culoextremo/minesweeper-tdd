using System.Collections;
using FluentAssertions;
using Kalendra.Commons.Runtime.Infraestructure.Services;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Kalendra.Commons.Tests.Runtime.Services
{
    public class RandomServicesTests
    {
        const int SomeSeed = 1500;

        [Test]
        public void SystemRandomService_SameSeedReturnsSameValues()
        {
            var sut1 = new SystemRandomService(SomeSeed);
            var sut2 = new SystemRandomService(SomeSeed);

            var resultsService1 = new[] {sut1.Next(), sut1.Next(), sut1.Next()};
            var resultsService2 = new[] {sut2.Next(), sut2.Next(), sut2.Next()};

            resultsService1.Should().BeEquivalentTo(resultsService2);
        }
        
        [Test]
        public void UnityRandomService_SameSeedReturnsSameValues()
        {
            var sut1 = new UnityEngineRandomService(SomeSeed);
            var sut2 = new UnityEngineRandomService(SomeSeed);

            var resultsService1 = new[] {sut1.Next(), sut1.Next(), sut1.Next()};
            var resultsService2 = new[] {sut2.Next(), sut2.Next(), sut2.Next()};

            resultsService1.Should().BeEquivalentTo(resultsService2);
        }
    }
}