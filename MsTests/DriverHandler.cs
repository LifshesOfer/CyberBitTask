using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace MsTests
{
    //Class to handle creation of WebDriver or RemoteWebDriver (if needed to run on multiple remote machines).
    //Can be easily expanded to support multiple browsers. 
    public static class DriverHandler
    {
        public static IWebDriver GetChromeDriver(ChromeOptions options)
        {
            IWebDriver driver = new ChromeDriver(options);
            driver.Manage().Window.Maximize();
            return driver;
        }

        public static IWebDriver GetChromeDriver()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            return driver;
        }
    }
}