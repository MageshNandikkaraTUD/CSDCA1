using Microsoft.VisualStudio.TestTools.UnitTesting;

// NuGet install Selenium WebDriver package and Support Classes
 
using OpenQA.Selenium;

// NuGet install Chrome Driver
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

// run 2 instances of VS to do run Selenium tests against localhost
// instance 1 : run web app e.g. on IIS Express
// instance 2 : from Test Explorer run Selenium test
// or use the dotnet vstest task
// e.g. dotnet vstest seleniumtest\bin\debug\netcoreapp2.1\seleniumtest.dll /Settings:seleniumtest.runsettings

namespace SeleniumTest
{
    [TestClass]
    public class UnitTest1
    {
        // .runsettings file contains test run parameters
        // e.g. URI for app
        // test context for this run

        private TestContext testContextInstance;

        // test harness uses this property to initliase test context
        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }

        // URI for web app being tested
        private String webAppUri;

        // .runsettings property overriden in vsts test runner 
        // release task to point to run settings file
        // also webAppUri overriden to use pipeline variable

        [TestInitialize]                // run before each unit test
        public void Setup()
        {
            // read URL from SeleniumTest.runsettings (configure run settings)
            this.webAppUri = testContextInstance.Properties["webAppUri"].ToString();
            
            //this.webAppUri = "https://mn-bpcalculator-staging.azurewebsites.net";
        }

        [TestMethod]
        public void TestBPUI()
        {

            String chromeDriverPath = Environment.GetEnvironmentVariable("ChromeWebDriver");
            if (chromeDriverPath is null)
            {
                chromeDriverPath = ".";                 // for IDE
            }
          
            using (IWebDriver driver = new ChromeDriver(chromeDriverPath))
            {
                // any exception below results in a test fail

                // navigate to URI for temperature converter
                // web app running on IIS express
                driver.Navigate().GoToUrl(webAppUri);

                // get weight in stone element
                IWebElement weightInStoneElement = driver.FindElement(By.Id("BP_Systolic"));
                // enter 10 in element
                weightInStoneElement.SendKeys("100");

                // get weight in stone element
                IWebElement weightInPoundsElement = driver.FindElement(By.Id("BP_Diastolic"));
                // enter 10 in element
                weightInPoundsElement.SendKeys("60");

                // submit the form
                driver.FindElement(By.CssSelector(".btn")).Submit();


                // explictly wait for result with "BPCategory" item
                IWebElement BPCategoryElement = new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                    .Until(c => c.FindElement(By.CssSelector("div.form-group:nth-child(4)")));
                
                // BPCategory "Ideal Blood Pressure"
                String bpcategory = BPCategoryElement.Text.ToString();

                //Assert
                StringAssert.EndsWith(bpcategory, "Ideal Blood Pressure");

                driver.Quit();

                // alternative - use Cypress or Playright
            }
        }
    }
}
