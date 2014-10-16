using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ploeh.AutoFixture;
using ServiceStack;
using Vehement.Process.Stalker.Core.Testing;
using Vehement.Process.Stalker.Diagnostics.ProcessMemory;
using Vehement.Process.Stalker.Services.Common.Request;
using Vehement.Process.Stalker.Services.Common.Response;

namespace Vehement.Process.Stalker.Diagnostics.Test.ProcessMemory
{
    [TestClass]
    public class ProcessCheckClientTest
    {
        private Fixture fixture;

        [TestInitialize]
        public void Initialise()
        {
            fixture = new Fixture();
        }

        [TestMethod]
        [TestCategory(TestCategory.Unit)]
        public void When_A_Valid_Check_Request_Should_Return_Ok()
        {
            //Arrange
            var restClient = new Mock<IRestClient>();
            var client = new ProcessCheckClient(restClient.Object);

            restClient.Setup(c => c.Get(It.IsAny<Check>())).Returns(new CheckResponse {Status = "Ok"});

            //Act
            CheckResponse actual = client.Check(new Check());

            //Assert
            restClient.Verify(c => c.Get(It.IsAny<Check>()), Times.Once);
            Assert.IsNotNull(actual);
            Assert.AreEqual("Ok", actual.Status);
        }

        [TestMethod]
        [TestCategory(TestCategory.Unit)]
        public void When_A_Valid_WildCardCheck_Request_Should_Return_Ok()
        {
            //Arrange
            var restClient = new Mock<IRestClient>();
            var client = new ProcessCheckClient(restClient.Object);

            restClient.Setup(c => c.Get(It.IsAny<WildCardCheck>())).Returns(new CheckResponse {Status = "Ok"});

            //Act
            CheckResponse actual = client.WildCardCheck(new WildCardCheck
            {
                WildCard = fixture.Create<string>()
            });

            //Assert
            restClient.Verify(c => c.Get(It.IsAny<WildCardCheck>()), Times.Once);
            Assert.IsNotNull(actual);
            Assert.AreEqual("Ok", actual.Status);
        }

        [TestMethod]
        [TestCategory(TestCategory.Unit)]
        [ExpectedException(typeof (WebServiceException))]
        public void When_Service_Throws_Error_Check_Method_Propogates()
        {
            //Arrange
            var restClient = new Mock<IRestClient>();
            var client = new ProcessCheckClient(restClient.Object);

            var wex = new WebServiceException("Could not find processes")
            {
                StatusCode = -1
            };

            restClient.Setup(c => c.Get(It.IsAny<Check>())).Throws(wex);

            //Act
            CheckResponse actual = client.Check(new Check());

            //Assert
            //todo - implement assert.expectedexception
            restClient.Verify(c => c.Get(It.IsAny<Check>()), Times.Once);
        }

        [TestMethod]
        [TestCategory(TestCategory.Unit)]
        [ExpectedException(typeof (WebServiceException))]
        public void Given_Service_Throws_Error_WildCardCheck_Method_Propogates()
        {
            //Arrange
            var restClient = new Mock<IRestClient>();
            var client = new ProcessCheckClient(restClient.Object);

            var wex = new WebServiceException("Could not find processes")
            {
                StatusCode = -1
            };

            restClient.Setup(c => c.Get(It.IsAny<WildCardCheck>())).Throws(wex);

            //Act
            CheckResponse actual = client.WildCardCheck(new WildCardCheck());

            //Assert
            //todo - implement assert.expectedexception
            restClient.Verify(c => c.Get(It.IsAny<Check>()), Times.Once);
        }
    }
}