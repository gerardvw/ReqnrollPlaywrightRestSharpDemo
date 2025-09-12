using ReqnrollPlaywrightRestSharpDemo.UI;
using ReqnrollPlaywrightRestSharpDemo.UI.Pages;
using System.Diagnostics.CodeAnalysis;

namespace ReqnrollPlaywrightRestSharpDemo.Context.Search
{
    [method: SetsRequiredMembers]
    public class SearchContextUI(UIDriver uiDriver) : ISearchContext
    {
        private readonly ProductsPage _productsPage = new(uiDriver);

        public Task AuthenticateUser(string user)
        {
            //No authentication and authorisation is applicable in this case, so this is a dummy
            return Task.CompletedTask;
        }

        public async Task SearchForItem(string searchTerm)
        {
            await _productsPage.Navigate(200, 299);
            await _productsPage.AcceptConsentIfVisible();
            await _productsPage.SearchForItem(searchTerm);
        }

        public async Task ValidateResult(string expectedDescription, string expectedPrice)
        {
            await _productsPage.ExpectProductToBeVisibleAsync([expectedDescription, expectedPrice]);
        }

        public async Task ValidateResultExpected(int expectedItems)
        {
            await _productsPage.ExpectProductCountAsync(expectedItems);
        }

        public async Task ValidateResultNotExpected(int notExpectedItems)
        {
            await _productsPage.ExpectNotProductCountAsync(notExpectedItems);
        }
    }
}
