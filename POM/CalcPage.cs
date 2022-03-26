//Usually a different project from main test execution framework.
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Linq;
using System.Threading;
using System.Collections.Generic;
public class CalcPage : Page<CalcPage>
{
    public const string PageUrl = "https://web2.0calc.com/";
    //Locators can be argued if should be public/internal/private based on intended use.
    //Generally prefer locators to be on the POM itself, as it's easier to quickly reference, as long as there aren't too many locators on the POM. 
    public static readonly By DisplayHistory = By.CssSelector("#hist");
    public static readonly By HistoryFrame = By.CssSelector("#histframe");
    public static readonly By HistoryRow = By.CssSelector($"{HistoryFrame.Criteria} li");
    public static By GetHistoryRowByIndex(int index) 
    {
        if(index < 1)
        {
            throw new System.Exception($"Row index cannot be less than {index}");
        }
        return By.CssSelector($"{HistoryRow.Criteria}:nth-child({index})");
    } 
    public static readonly By TextInput = By.CssSelector("input#input");
    public static readonly By CalcResult = By.Id("result");
    //Should be an enum of possible values.
    public static By GetButtonById(string buttonId) => By.CssSelector($"button#Btn{buttonId}");

    public CalcPage(IWebDriver driver) : base(driver)
    {

    }

    public static CalcPage NavigateToPage(IWebDriver driver)
    {
        driver.Navigate().GoToUrl(PageUrl);
        return new CalcPage(driver);
    }

    public void WaitForLoading(System.TimeSpan? timeOut = null) {
        timeOut ??= System.TimeSpan.FromSeconds(30);
        WebDriverWait driverWait = new(this.Driver, timeOut.Value);
        //Waits until TextInput is no longer loading.
        driverWait.Until(driver => {
            bool isHidden1 = driver.FindElements(By.CssSelector($"{TextInput.Criteria}.loading")).Any();
            Thread.Sleep(200);
            bool isHidden2 = driver.FindElements(By.CssSelector($"{TextInput.Criteria}.loading")).Any();
            return !isHidden1 && !isHidden2;
        });
    }
    public void ClickButton(string buttonId)
    {
        this.Driver.FindElement(GetButtonById(buttonId)).Click();
        WaitForLoading();
    }
    public void ClearInput()
    {
        ClickButton("Clear");
    }
    public void EnterNumber(int number)
    {
        foreach(int digit in NumberToArray(number))
        {
            ClickButton(digit.ToString());
        }
    }

    private int[] NumberToArray(int value)
    {
        Stack<int> numbers = new Stack<int>();

        for(; value > 0; value /= 10)
            numbers.Push(value % 10);

        return numbers.ToArray();
    }
    public string GetCalcResult()
    {
        return this.Driver.FindElement(TextInput).GetAttribute("value");
    }
    public List<HistoryRow> GetHistoryLog()
    {
        List<HistoryRow> historyRows = new();
        int numOfRows = this.Driver.FindElements(HistoryRow).Count();
        for(int i=1; i<=numOfRows; i++)
        {
            string formula = this.Driver.FindElement(By.CssSelector($"{GetHistoryRowByIndex(i).Criteria} > p:last-child")).GetAttribute("data-inp");
            string result = this.Driver.FindElement(By.CssSelector($"{GetHistoryRowByIndex(i).Criteria} > p:first-child")).GetAttribute("title");
            historyRows.Add(new HistoryRow(formula, result));
        }
        return historyRows;
    }

    public void ClickDisplayHistory()
    {
        this.Driver.FindElement(DisplayHistory).Click();
    }
}