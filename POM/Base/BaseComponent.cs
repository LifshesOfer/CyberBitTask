using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support;
using OpenQA.Selenium.Support.UI;

namespace POM
{
    //Additional basic functionality goes here.
    public abstract class BaseComponent
    {
        public IWebDriver Driver;

        protected BaseComponent(IWebDriver driver)
        {
            this.Driver = driver;
        }

        public void MoveToElement(By locator)
        {
            Actions actions = new(Driver);
            actions.MoveToElement(Driver.FindElement(locator)).Build().Perform();
        }

        public void WaitForElementVisible(By locator, TimeSpan timeOut)
        {
            WebDriverWait driverWait = new(Driver, timeOut);
            driverWait.Until(_driver => _driver.FindElement(locator).Displayed);
        }
    }
}
