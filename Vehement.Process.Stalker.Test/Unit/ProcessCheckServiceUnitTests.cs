using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ploeh.AutoFixture;
using Vehement.Process.Stalker.Core.Testing;
using Vehement.Process.Stalker.Services.Common;
using Vehement.Process.Stalker.Services.Common.Request;
using Vehement.Process.Stalker.Services.Common.Response;
using Vehement.Process.Stalker.Services.Memory;
using Vehement.Process.Stalker.Services.ServiceStack;

namespace Vehement.Process.Stalker.Test.Unit
{
    [TestClass]
    public class ProcessCheckServiceUnitTests
    {
        private Fixture fixture;

        [TestInitialize]
        public void Initialise()
        {
            fixture = new Fixture();
        }

        [TestMethod, TestCategory(TestCategory.Unit)]
        public void Given_A_Valid_Check_Request_Should_Return_Valid_Response()
        {
            //Arrange
            var memoryServices = new Mock<IMemoryServices>();
            var processCheckService = new ProcessCheckService(memoryServices.Object);

            var expected = new List<ProcessInformation>
            {
                new ProcessInformation
                {
                    ProcessName = fixture.Create<string>(),
                    ExecutingLocation = fixture.Create<string>(),
                    PrivateMemorySize64 = fixture.Create<long>(),
                    WorkingSet64 = fixture.Create<long>()
                }
            };

            memoryServices.Setup(m => m.GetProcesses()).Returns(expected);

            //Act
            var actual = (CheckResponse) processCheckService.Get(new Check());

            //Assert
            Assert.AreEqual(1, actual.Processes.Count);
            Assert.AreEqual(expected[0], actual.Processes[0]);
            
        }

        [TestMethod, TestCategory(TestCategory.Unit)]
        public void Given_A_Valid_WildCardCheck_Request_Should_Return_Valid_Response()
        {
            //Arrange
            var memoryServices = new Mock<IMemoryServices>();
            var processCheckService = new ProcessCheckService(memoryServices.Object);
            const string processWildCard = "My.Process.Namespace";

            var expected = new List<ProcessInformation>
            {
                new ProcessInformation
                {
                    ProcessName = fixture.Create(processWildCard),
                    ExecutingLocation = fixture.Create<string>(),
                    PrivateMemorySize64 = fixture.Create<long>(),
                    WorkingSet64 = fixture.Create<long>()
                }
            };

            memoryServices.Setup(m => m.GetProcesses(It.IsAny<string>())).Returns(expected);

            var wildcardCheck = new WildCardCheck
            {
                WildCard = processWildCard
            };

            //Act
            var actual = (CheckResponse)processCheckService.Get(wildcardCheck);

            //Assert
            Assert.AreEqual(1, actual.Processes.Count);
            Assert.AreEqual(expected[0], actual.Processes[0]);
            
        }
    }
}