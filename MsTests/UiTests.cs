using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using POM;
using System.Text.RegularExpressions;

namespace MsTests
{
    [TestClass]
    public class UiTests
    {
        private IWebDriver driver;
        private Catalog catalog;
        [TestInitialize]
        public void TestInitialize(){
            driver = MsTests.DriverHandler.GetChromeDriver();
            // driver.Navigate().GoToUrl("http://automationpractice.com/index.php");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromMinutes(2);
            catalog = new(driver);
            catalog.NavigateToPage();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            driver.Close();
            driver.Quit();
        }

        //IMO should be 3 separate tests (Generally, Assert = 1 Test)
        [TestMethod]
        public void AddToCatalog()
        {
            var item = new CatalogItem(1, driver);
            // string expectedName = item.GetProductName();
            var layerCart = catalog.AddToCartByIndex(item.ItemIndex);

            Assert.AreEqual("Product successfully added to your shopping cart", layerCart.GetSuccessMessage());
            
            Assert.AreEqual(item.GetProductName(), layerCart.GetAddedProductName());
            var match = Regex.Match(item.GetProductPrice(), @"([-+]?[0-9]*\.?[0-9]+)");
            double price = Convert.ToSingle(match.Groups[1].Value);
            double expectedPrice = layerCart.GetProductQuantity() * GetPriceFromString(item.GetProductPrice());
            
           
            
            Assert.AreEqual(expectedPrice, GetPriceFromString(layerCart.GetTotalProducts()));
        }

        public double GetPriceFromString(string currencyStr)
        {
            var match = Regex.Match(currencyStr, @"([-+]?[0-9]*\.?[0-9]+)");
            double price = Convert.ToSingle(match.Groups[1].Value);

            return double.Parse(price.ToString("#.##"));
        }
    }
}
