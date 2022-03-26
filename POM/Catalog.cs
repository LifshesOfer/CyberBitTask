using OpenQA.Selenium;

namespace POM
{
    public class Catalog : Page<Catalog>
    {
        public static readonly By CatalogContainer = By.CssSelector("#homefeatured");
        public static readonly By CatalogItems = By.CssSelector($"{CatalogContainer.Criteria} li");
        public Catalog(IWebDriver driver) : base("http://automationpractice.com/index.php", driver)
        {

        }

        public LayerCart AddToCartByIndex(int index)
        {
            CatalogItem item = new(index, Driver);
            item.AddToCart();
            return new LayerCart(Driver);
        }
    }
}