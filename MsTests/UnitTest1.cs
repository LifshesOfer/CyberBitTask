using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using POM;

namespace MsTests
{
    [TestClass]
    public class UnitTest1
    {
        private IWebDriver driver;
        private CalculatorTests calcTests;
        [TestInitialize]
        public void TestInitialize(){
            driver = DriverHandler.GetChromeDriver();
            
            calcTests = new(driver);
            calcTests.NavigateToCalcPage();
        }
        [TestMethod]
        public void TestPlus()
        {
            calcTests.TestPlusFormula(3,4);
        }

        [TestMethod]
        public void TestMinus()
        {
            calcTests.TestMinusFormula(6,4);
        }

        [TestCleanup]
        public void Cleanup(){
            driver.Close();
            driver.Quit();
        }
    }
}
