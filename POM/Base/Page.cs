using OpenQA.Selenium;

namespace POM
{
    //Additional basic _Page_ functionality goes here.
    public abstract class Page<T> : BaseComponent where T : Page<T>
    {
        public string PageUrl{get;protected set;}
        protected Page(string pageUrl, IWebDriver driver) : base(driver)
        {
            PageUrl = pageUrl;
        }

        //Generally would return the page T, but requires setting up an instance creator.
        public void NavigateToPage()
        {
            this.Driver.Navigate().GoToUrl(PageUrl);
        }
    }
}
