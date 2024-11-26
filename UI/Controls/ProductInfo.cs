using Microsoft.Playwright;

namespace ReqnrollPlaywrightRestSharpDemo.UI.Controls
{
    public class ProductInfo(ILocator parent)
    {
        public ILocator Self = parent;

        public ILocator AddToCart()
        {
            return Self.Locator(".add-to-cart");
        }
    }
}
