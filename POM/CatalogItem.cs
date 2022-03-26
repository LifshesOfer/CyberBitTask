using OpenQA.Selenium;

namespace POM
{
    public class CatalogItem : BaseComponent
    {
        public int ItemIndex {get;}
        public By ItemByIndex => By.CssSelector($"{Catalog.CatalogItems.Criteria}:nth-child({ItemIndex}) .product-container");
        public By ItemName => By.CssSelector($"{ItemByIndex.Criteria} [itemprop=name] .product-name");
        public By ItemPrice => By.CssSelector($"{ItemByIndex.Criteria} .right-block [itemprop='price']");
        public By ItemAddToCart => By.CssSelector($"{ItemByIndex.Criteria} .button-container [title='Add to cart']");
        public CatalogItem(int itemIndex, IWebDriver driver) : base(driver)
        {
            ItemIndex = itemIndex;
        }

        public void AddToCart()
        {
            this.MoveToElement(ItemByIndex);
            this.Driver.FindElement(ItemAddToCart).Click();
        }
        public string GetProductName()
        {
            return this.Driver.FindElement(ItemName).GetAttribute("title");
        }

        public string GetProductPrice()
        {
            return this.Driver.FindElement(ItemPrice).Text;
        }
    }
}