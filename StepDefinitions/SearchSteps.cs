using Microsoft.Playwright;
using ReqnrollPlaywrightRestSharpDemo.Context;
using ReqnrollPlaywrightRestSharpDemo.UI.Pages;

namespace ReqnrollPlaywrightRestSharpDemo.StepDefinitions
{
    [Binding]
    public class SearchSteps
    {
        private readonly IReqnrollOutputHelper _reqnrollOutputHelper;
        private readonly ScenarioContext _scenarioContext;
        private readonly ScenarioContextUI _scenarioContextUI;

        private ProductsPage _productsPage;

        public SearchSteps(IReqnrollOutputHelper outputHelper, ScenarioContext scenarioContext, ScenarioContextUI scenarioContextUI)
        {
            _scenarioContextUI = scenarioContextUI;
            _scenarioContext = scenarioContext;
            _reqnrollOutputHelper = outputHelper;

            _productsPage = new ProductsPage(_scenarioContextUI.BaseUrl, _scenarioContextUI.Page);
        }

        [Given("{word} is authenticated and authorised for searching products")]
        public async Task UserIsAuthenticatedAndAuthorisedForSearchingProducts(string user)
        {
            //No authentication and authorisation is applicable in this case, so only navigating to page
            var response = await _productsPage.Navigate();

            response.Should().NotBeNull();
            response!.Status.Should().BeInRange(200, 299);

            await _productsPage.AcceptConsentIfVisible();
        }

        [When("{word} searches for {string}")]
        public void UserSearchesFor(string user, string searchTerm)
        {
            _productsPage.SearchForItem(searchTerm);
        }

        [Then("An item with description {string} and a price {string} should be returned")]
        public async Task AnItemWithDescriptionAndAPriceShouldBeReturned(string expectedDescription, string expectedPrice)
        {
            var productInfoItems = _productsPage.ProductInfoItemsFiltered([expectedDescription, expectedPrice]);

            await Assertions.Expect(productInfoItems).ToBeVisibleAsync();
        }

        [Then("No items should be returned")]
        public async Task NoItemsShouldBeReturned()
        {
            var productInfoList = _productsPage.ProductInfoList();

            await Assertions.Expect(productInfoList).ToHaveCountAsync(0);
        }

        [Then("There should be items returned")]
        public async Task AllItemsShouldBeReturned()
        {
            var productInfoList = _productsPage.ProductInfoList();

            await Assertions.Expect(productInfoList).Not.ToHaveCountAsync(0);
        }
    }
}
