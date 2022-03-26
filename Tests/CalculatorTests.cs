using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;

namespace POM
{
    public class CalculatorTests
    {

        private IWebDriver driver;
        private CalcPage calcPage;
        public CalculatorTests(IWebDriver driver)
        {
            this.driver = driver;
        }
        public void NavigateToCalcPage()
        {
            calcPage = CalcPage.NavigateToPage(driver);
        }

        public void StartTests()
        {
            NavigateToCalcPage();
            List<Double> results = new();
            results.Add(TestPlusFormula(2, 3));
            results.Add(TestMinusFormula(10, 2));
            results.Add(TestSinFormula(30));
            results.Reverse();
            TestResultsHistory(results);
        }
        public double TestPlusFormula(int firstNum, int secondNum)
        {
            calcPage.ClearInput();
            calcPage.EnterNumber(firstNum);
            calcPage.ClickButton("Plus");
            calcPage.EnterNumber(secondNum);
            calcPage.ClickButton("Calc");

            int result = int.Parse(calcPage.GetCalcResult());
            int expected = firstNum + secondNum;
            if (result != expected)
            {
                throw new System.Exception($"Result of '{firstNum} + {secondNum}' did not equal expected: {expected}");
            }
            return result;
        }
        public double TestSinFormula(double angle)
        {
            calcPage.ClearInput();
            calcPage.EnterNumber(Convert.ToInt32(angle));
            calcPage.ClickButton("Sin");
            calcPage.ClickButton("Calc");

            double result = double.Parse(calcPage.GetCalcResult());
            double expected = Math.Round(Math.Sin(angle * (Math.PI / 180)), 2);
            //Math.Sin returns in radians, no strength to work on that.
            if (result != expected)
            {
                throw new System.Exception($"Result of 'Sin({angle})' did not equal expected: {expected}");
            }
            return result;
        }
        public double TestMinusFormula(int firstNum, int secondNum)
        {
            calcPage.ClearInput();
            calcPage.EnterNumber(firstNum);
            calcPage.ClickButton("Minus");
            calcPage.EnterNumber(secondNum);
            calcPage.ClickButton("Calc");

            int result = int.Parse(calcPage.GetCalcResult());
            int expected = firstNum - secondNum;
            if (result != expected)
            {
                throw new System.Exception($"Result of '{firstNum} - {secondNum}' did not equal expected: {expected}");
            }
            return result;
        }
        public void TestResultsHistory(List<Double> expectedResults)
        {
            calcPage.ClickDisplayHistory();
            List<Double> actualResults = new();
            calcPage.GetHistoryLog().ForEach(row => actualResults.Add(double.Parse(row.Result)));

            if (!actualResults.SequenceEqual(expectedResults))
            {
                throw new System.Exception("Assertion except");
            }
        }
    }
}
