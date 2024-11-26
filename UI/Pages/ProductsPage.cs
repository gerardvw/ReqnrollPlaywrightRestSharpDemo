using Microsoft.Playwright;
using ReqnrollPlaywrightRestSharpDemo.UI.Controls;

namespace ReqnrollPlaywrightRestSharpDemo.UI.Pages
{
    public class ProductsPage(string baseUrl, IPage page) : BasePage(baseUrl, page)
    {
        private ILocator SearchFor() => Page.Locator("#search_product");
        private ILocator Submit() => Page.Locator("#submit_search");

        protected override string RelativeUri => "/products";

        public void SearchForItem(String searchTerm)
        {
            SearchFor().FillAsync(searchTerm);
            Submit().ClickAsync();
        }

        public ProductInfo ProductInfo(String expectedDescription, String expectedPrice)
        {
            return new ProductInfo(
                    Page.Locator(".productinfo")
                            .Filter(new LocatorFilterOptions() { HasText = expectedDescription })
                            .Filter(new LocatorFilterOptions() { HasText = expectedPrice })
            );
        }
    }
}

