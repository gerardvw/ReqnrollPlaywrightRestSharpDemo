using Microsoft.Playwright;

namespace ReqnrollPlaywrightRestSharpDemo.UI.Controls
{
    public class ProductInfo(ILocator self)
    {
        public ILocator Self = self;

        public ILocator AddToCart()
        {
            return Self.Locator(".add-to-cart");
        }
    }
}
