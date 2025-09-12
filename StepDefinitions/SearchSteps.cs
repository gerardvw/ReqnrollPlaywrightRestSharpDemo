using ReqnrollPlaywrightRestSharpDemo.Context.Search;

namespace ReqnrollPlaywrightRestSharpDemo.StepDefinitions
{
    [Binding]
    public class SearchSteps(ISearchContext searchContext)
    {
        [Given("{word} is authenticated and authorised for searching products")]
        public async Task UserIsAuthenticatedAndAuthorisedForSearchingProducts(string user)
        {
            await searchContext.AuthenticateUser(user);
        }

        [When("{word} searches for {string}")]
        public async Task UserSearchesFor(string user, string searchTerm)
        {
            await searchContext.SearchForItem(searchTerm);
        }

        [Then("An item with description {string} and a price {string} should be returned")]
        public async Task AnItemWithDescriptionAndAPriceShouldBeReturned(string expectedDescription, string expectedPrice)
        {
            await searchContext.ValidateResult(expectedDescription, expectedPrice);
        }

        [Then("No items should be returned")]
        public async Task NoItemsShouldBeReturned()
        {
            await searchContext.ValidateResultExpected(0);
        }

        [Then("There should be items returned")]
        public async Task ThereShouldBeItemsReturned()
        {
            await searchContext.ValidateResultNotExpected(0);
        }
    }
}
