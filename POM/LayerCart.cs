using System;
using OpenQA.Selenium;


namespace POM
{    
    public class LayerCart : BaseComponent
    {
        public static readonly By LayerContainer = By.CssSelector("#layer_cart");
        public static readonly By SuccessMessage = By.CssSelector($"{LayerContainer.Criteria} .layer_cart_product h2");
        public static readonly By AddedProductQty = By.CssSelector($"{LayerContainer.Criteria} #layer_cart_product_quantity");
        public static readonly By TotalProducts = By.CssSelector($"{LayerContainer.Criteria} .layer_cart_cart .ajax_block_products_total");
        public static readonly By CheckoutButton = By.CssSelector($"{LayerContainer.Criteria} .layer_cart_cart [title='Proceed to checkout']");
        public static readonly By AddedProductName = By.CssSelector($"{LayerContainer.Criteria} #layer_cart_product_title");
        public LayerCart(IWebDriver driver) : base(driver)
        {
            WaitForPopup();
        }

        public void WaitForPopup(TimeSpan? timeOut = null)
        {
            timeOut ??= TimeSpan.FromSeconds(30);
            this.WaitForElementVisible(LayerContainer, timeOut.Value);
        }

        public string GetTotalProducts()
        {
            return this.Driver.FindElement(TotalProducts).Text;
        }

        public string GetSuccessMessage()
        {
            return this.Driver.FindElement(SuccessMessage).Text;
        }

        public void ProceedToCheckout()
        {
            this.Driver.FindElement(CheckoutButton).Click();
        }

        public string GetAddedProductName()
        {
            return this.Driver.FindElement(AddedProductName).Text;
        }

        public int GetProductQuantity()
        {
            return int.Parse(this.Driver.FindElement(AddedProductQty).Text);
        }

    }
}