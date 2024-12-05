using Microsoft.Playwright;

namespace ReqnrollPlaywrightRestSharpDemo.UI.Pages
{
    public class ProductsPage(UIDriver uiDriver) : BasePage(uiDriver)
    {
        private ILocator SearchFor() => UIDriver.Page.Locator("#search_product");
        private ILocator Submit() => UIDriver.Page.Locator("#submit_search");

        protected override string RelativeUri => "/products";
        
        public ILocator ProductInfoList() => UIDriver.Page.Locator(".productinfo");

        public async Task SearchForItem(string searchTerm)
        {
            await SearchFor().FillAsync(searchTerm);
            await Submit().ClickAsync();
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

