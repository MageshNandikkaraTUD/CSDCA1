using Microsoft.VisualStudio.TestTools.UnitTesting;
using BPCalculator;

namespace BPUnitTestProject
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            BloodPressure BP = new BloodPressure() { Systolic = 110, Diastolic = 70 };
            Assert.AreEqual(bmi.BPCategory, BPCategory.Ideal);
        }

    }
}