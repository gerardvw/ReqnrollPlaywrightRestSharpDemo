using Microsoft.Playwright;

namespace ReqnrollPlaywrightRestSharpDemo.UI.Pages
{
    public class ProductsPage(UIDriver uiDriver) : BasePage(uiDriver)
    {
        private ILocator SearchFor() => UIDriver.Page.Locator("#search_product");
        private ILocator Submit() => UIDriver.Page.Locator("#submit_search");
        private ILocator ProductInfoList() => UIDriver.Page.Locator(".productinfo");
        private ILocator ProductInfoListFiltered(params string[] expectedTexts)
            => expectedTexts.Aggregate(ProductInfoList(),
                (locator, text) => locator.Filter(new LocatorFilterOptions { HasText = text }));

        protected override string RelativeUri => "/products";

        public async Task SearchForItem(string searchTerm)
        {
            await SearchFor().FillAsync(searchTerm);
            await Submit().ClickAsync();
        }

        public async Task ExpectProductCountAsync(int expectedCount)
        {
            var productInfoList = ProductInfoList();

            await Assertions.Expect(productInfoList).ToHaveCountAsync(expectedCount);
        }

        public async Task ExpectNotProductCountAsync(int notExpectedItems)
        {
            var productInfoList = ProductInfoList();

            await Assertions.Expect(productInfoList).Not.ToHaveCountAsync(notExpectedItems);
        }

        public async Task ExpectProductToBeVisibleAsync(string[] expectedTexts)
        {
            var productInfoItems = ProductInfoListFiltered(expectedTexts);

            await Assertions.Expect(productInfoItems).ToBeVisibleAsync();
        }
    }
}

