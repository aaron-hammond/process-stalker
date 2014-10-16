using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vehement.Process.Stalker.Core.Testing;
using Vehement.Process.Stalker.Services.Common.Request;
using Vehement.Process.Stalker.Services.Common.Response;
using Vehement.Process.Stalker.Services.Memory;
using Vehement.Process.Stalker.Services.ServiceStack;

namespace Vehement.Process.Stalker.Test.Integration
{
    [TestClass]
    public class ProcessCheckServiceIntegrationTests
    {
        [TestMethod, TestCategory(TestCategory.Integration)]
        public void Given_A_Valid_Check_Request_Should_Return_Valid_Response()
        {
            var memoryServices = new MemoryServices();
            var processCheckService = new ProcessCheckService(memoryServices);

            var actual = (CheckResponse)processCheckService.Get(new Check());
            Assert.IsNotNull(actual.Processes);

        }

        [TestMethod, TestCategory(TestCategory.Integration)]
        public void Given_A_Valid_WildCardCheck_Request_Should_Return_Valid_Response()
        {
            var memoryServices = new MemoryServices();
            var processCheckService = new ProcessCheckService(memoryServices);

            var wildcardCheck = new WildCardCheck
            {
                WildCard = "devenv"
            };

            var actual = (CheckResponse)processCheckService.Get(wildcardCheck);
            Assert.IsNotNull(actual.Processes);

        }
    }
}
