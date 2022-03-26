using OpenQA.Selenium;


//Additional basic functionality goes here.
public abstract class Page<T> where T: Page<T>
{
    public IWebDriver Driver;

    protected Page(IWebDriver driver)
    {
        this.Driver = driver;
    }
    
}