using Microsoft.Playwright;
using ReqnrollPlaywrightRestSharpDemo.UI.Controls;

namespace ReqnrollPlaywrightRestSharpDemo.UI.Pages
{
    public class ProductsPage(string baseUrl, IPage page) : BasePage(baseUrl, page)
    {
        private ILocator SearchFor() => Page.Locator("#search_product");
        private ILocator Submit() => Page.Locator("#submit_search");

        protected override string RelativeUri => "/products";
        
        public ILocator ProductInfoList() => Page.Locator(".productinfo");

        public void SearchForItem(String searchTerm)
        {
            SearchFor().FillAsync(searchTerm);
            Submit().ClickAsync();
        }

        public ILocator ProductInfoItemsFiltered(string[] expectedTexts)
        {
            var filteredLocator = ProductInfoList();
            foreach (var text in expectedTexts)
            {
                // Sequentially filter the locator for each expected text
                filteredLocator = filteredLocator.Filter(new LocatorFilterOptions { HasText = text });
            }

            return filteredLocator;
        }
    }
}

