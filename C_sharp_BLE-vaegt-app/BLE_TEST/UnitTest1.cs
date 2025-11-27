using Microsoft.VisualStudio.TestTools.UnitTesting;
using BLE_vaegt_app;    
using Microsoft.Maui.Controls;
using BLE_vaegt_app;

namespace BLE_TEST
{
    // Enkel mock af Label
    public class MockLabel
    {
        public string Text { get; set; }
    }

    [TestClass]
    public class MeasurementHandlerTests
    {
        [TestMethod]
        public void HandleIncomingData_ValidData_ShouldUpdateLabelsAndGlobalData()
        {
            // Arrange
            var handler = new MeasurementHandler();

            var weightLabel = new MockLabel();
            var logLabel = new MockLabel();
            var fileLabel = new MockLabel();

            string testData = "BLE:75.3";

            // Act
            handler.HandleIncomingData(testData, weightLabel, logLabel, fileLabel);

            // Assert
            Assert.AreEqual("BLE75.3", weightLabel.Text);
            Assert.IsFalse(string.IsNullOrEmpty(logLabel.Text), "Log should not be empty");
            Assert.IsFalse(string.IsNullOrEmpty(fileLabel.Text), "File path should not be empty");
            Assert.IsTrue(GlobalData.SkemaData.Count > 0, "GlobalData should contain entries");
        }

        [TestMethod]
        public void HandleIncomingData_InvalidData_ShouldSetErrorText()
        {
            // Arrange
            var handler = new MeasurementHandler();

            var weightLabel = new MockLabel();
            var logLabel = new MockLabel();
            var fileLabel = new MockLabel();

            string testData = "INVALID_DATA";

            // Act
            handler.HandleIncomingData(testData, weightLabel, logLabel, fileLabel);

            // Assert
            Assert.IsTrue(weightLabel.Text.StartsWith("Fejl ved databehandling"), "Weight label should show error");
        }
    }
}

