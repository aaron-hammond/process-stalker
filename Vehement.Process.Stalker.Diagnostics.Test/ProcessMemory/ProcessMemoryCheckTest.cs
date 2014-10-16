
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ploeh.AutoFixture;
using Vehement.Process.Stalker.Core.Testing;
using Vehement.Process.Stalker.Diagnostics.ProcessMemory;
using Vehement.Process.Stalker.Diagnostics.ProcessMemory.Config;
using Vehement.Process.Stalker.Services.Common;
using Vehement.Process.Stalker.Services.Common.Request;
using Vehement.Process.Stalker.Services.Common.Response;

namespace Vehement.Process.Stalker.Diagnostics.Test.ProcessMemory
{
    [TestClass]
    public class ProcessMemoryCheckTest
    {

        private Fixture fixture;

        [TestInitialize]
        public void Initialise()
        {
            fixture = new Fixture();   
        }

        [TestMethod, TestCategory(TestCategory.Unit)]
        [ExpectedException(typeof(ProcessCheckExceededMaximumSetException))]
        public void When_Check_Memory_Too_High_Should_Throw_Exception()
        {
            //Arrange
            var processSettings = new Mock<IProcessCheckSettings>();
            var processCheckClient = new Mock<IProcessCheckClient>();

            const long currentProcessMemory = 600;
            const int maximumMemoryLimit = 400;

            var processName = fixture.Create<string>();
            var executingLocation = fixture.Create<string>();

            var processMonitorCollection = new ProcessCheckCollection
            {
                new ProcessCheckElement
                {
                    ProcessName = processName,
                    MaxMemory = maximumMemoryLimit,
                    ExecutingLocation = executingLocation
                }
            };

            var check = new ProcessMemoryCheck(processSettings.Object, processCheckClient.Object);

            processSettings.Setup(s => s.Settings).Returns(processMonitorCollection);

            processCheckClient.Setup(p => p.WildCardCheck(It.IsAny<WildCardCheck>())).Returns(new CheckResponse
            {
                Processes = new List<ProcessInformation>()
                        {
                            new ProcessInformation
                            {
                                ProcessName = processName,
                                ExecutingLocation = executingLocation,
                                WorkingSet64 = currentProcessMemory
                            }
                        }
            });

            //Act
            check.DoWildCardCheck(DateTime.Now);
     
        }

        [TestMethod, TestCategory(TestCategory.Unit)]
        public void DoWork_Check_Process_Memory_Consumption_Process_Memory_Not_Too_High_Should_Not_Throw_Exception()
        {
            //Arrange
            var processSettings = new Mock<IProcessCheckSettings>();
            var processCheckClient = new Mock<IProcessCheckClient>();

            const long currentProcessMemory = 300;
            const int maximumMemoryLimit = 400;

            var processName = fixture.Create<string>();
            var executingLocation = fixture.Create<string>();

            var processMonitorCollection = new ProcessCheckCollection
            {
                new ProcessCheckElement
                {
                    ProcessName = processName,
                    MaxMemory = maximumMemoryLimit,
                    ExecutingLocation = executingLocation
                }
            };

            var check = new ProcessMemoryCheck(processSettings.Object, processCheckClient.Object);

            processSettings.Setup(s => s.Settings).Returns(processMonitorCollection);

            processCheckClient.Setup(p => p.WildCardCheck(It.IsAny<WildCardCheck>())).Returns(new CheckResponse
            {
                Processes = new List<ProcessInformation>()
                        {
                            new ProcessInformation
                            {
                                ProcessName = processName,
                                ExecutingLocation = executingLocation,
                                WorkingSet64 = currentProcessMemory
                            }
                        }
            });

            //Act
            check.DoWildCardCheck(DateTime.Now);
        }

        [TestMethod, TestCategory(TestCategory.Unit)]
        [ExpectedException(typeof(ProcessCheckExceededMaximumSetException))]
        public void DoWork_Check_Process_Memory_Consumption_Process_Memory_Varying_Should_Throw_Exception()
        {
            //Arrange
            var processSettings = new Mock<IProcessCheckSettings>();
            var processCheckClient = new Mock<IProcessCheckClient>();

            const long currentProcessMemory = 300;
            const int maximumMemoryLimit1 = 400;
            const int maximumMemoryLimit2 = 100;

            var processName1 = fixture.Create<string>();
            var processName2 = fixture.Create<string>();
            var executingLocation = fixture.Create<string>();

            var processMonitorCollection = new ProcessCheckCollection
            {
                new ProcessCheckElement
                {
                    ProcessName = processName1,
                    MaxMemory = maximumMemoryLimit1,
                    ExecutingLocation = executingLocation
                },
                
                new ProcessCheckElement
                {
                    ProcessName = processName2,
                    MaxMemory = maximumMemoryLimit2,
                    ExecutingLocation = executingLocation
                }
            };

            var check = new ProcessMemoryCheck(processSettings.Object, processCheckClient.Object);

            processSettings.Setup(s => s.Settings).Returns(processMonitorCollection);

            processCheckClient.Setup(p => p.WildCardCheck(It.IsAny<WildCardCheck>())).Returns(new CheckResponse
            {
                Processes = new List<ProcessInformation>()
                        {
                            new ProcessInformation
                            {
                                ProcessName = processName1,
                                ExecutingLocation = executingLocation,
                                WorkingSet64 = currentProcessMemory
                            },
                        
                            new ProcessInformation
                            {
                                ProcessName = processName2,
                                ExecutingLocation = executingLocation,
                                WorkingSet64 = currentProcessMemory
                            }
                        }
            });

            //Act
            check.DoWildCardCheck(DateTime.Now);
        }
    }
}