using Microsoft.VisualStudio.TestTools.UnitTesting;
using BPCalculator;

namespace BPUnitTestProject
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void UnitTestLow()
        {
            BloodPressure BP = new() { Systolic = 80, Diastolic = 50 };
            Assert.AreEqual(BP.Category, BPCategory.Low);
        }

        [TestMethod]
        public void UnitTestIdeal()
        {
            BloodPressure BP = new() { Systolic = 110, Diastolic = 70 };
            Assert.AreEqual(BP.Category, BPCategory.Ideal);
        }

        [TestMethod]
        public void UnitTestPreHighScenario1()
        {
            BloodPressure BP = new() { Systolic = 130, Diastolic = 75 };
            Assert.AreEqual(BP.Category, BPCategory.PreHigh);
        }

        [TestMethod]
        public void UnitTestPreHighScenario2()
        {
            BloodPressure BP = new() { Systolic = 110, Diastolic = 85 };
            Assert.AreEqual(BP.Category, BPCategory.PreHigh);
        }

        [TestMethod]
        public void UnitTestHigh()
        {
            BloodPressure BP = new() { Systolic = 170, Diastolic = 95 };
            Assert.AreEqual(BP.Category, BPCategory.High);
        }

    }
}




