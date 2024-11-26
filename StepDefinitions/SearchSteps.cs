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

        [Given("products page is opened")]
        public async Task GivenProductsPageIsOpened()
        {
            var response = await _productsPage.Navigate();

            response.Should().NotBeNull();
            response!.Status.Should().BeInRange(200, 299);
        }

        [When("I search for {string}")]
        public void WhenISearchFor(string searchTerm)
        {
            throw new PendingStepException();
        }

        [Then("I should see an item with description {string} and a price {string}")]
        public void ThenIShouldSeeAnItemWithDescriptionAndAPrice(string description, string price)
        {
            throw new PendingStepException();
        }

        [Then("it should be possible to add this item to my cart")]
        public void ThenItShouldBePossibleToAddThisItemToMyCart()
        {
            throw new PendingStepException();
        }
    }
}
