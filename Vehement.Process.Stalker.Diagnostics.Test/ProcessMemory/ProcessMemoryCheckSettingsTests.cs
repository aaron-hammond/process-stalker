#region

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vehement.Process.Stalker.Core.Testing;
using Vehement.Process.Stalker.Diagnostics.ProcessMemory;

#endregion

namespace Vehement.Process.Stalker.Diagnostics.Test.ProcessMemory
{
    [TestClass]
    public class ProcessMemoryCheckSettingsTests
    {
        [TestMethod, TestCategory(TestCategory.Integration)]
        public void When_Calling_Settings_Property_Should_Return_Settings()
        {
            //Arrange
            var processMemory = new ProcessCheckSettings();
            const string expectedProcessName = "Process.Stalker";
            const string expectedLocationName = @"C:\Process.Staker.exe";
            const double maximumProcessMemory = 0;

            //Act
            var settings = processMemory.Settings;

            //Assert
            Assert.AreEqual(settings[0].ProcessName, expectedProcessName);
            Assert.AreEqual(settings[0].MaxMemory, maximumProcessMemory);
            Assert.AreEqual(settings[0].ExecutingLocation, expectedLocationName);
        }
    }
}