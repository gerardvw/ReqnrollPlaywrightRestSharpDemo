using ReqnrollPlaywrightRestSharpDemo.API;
using ReqnrollPlaywrightRestSharpDemo.API.Clients;

namespace ReqnrollPlaywrightRestSharpDemo.Context.Search
{
    public class SearchContextAPI(APIDriver apiDriver) : ISearchContext
    {
        private readonly SearchProductAPI _searchProduct = new(apiDriver.RestClient);

        public Task AuthenticateUser(string user)
        {
            //No authentication and authorisation is applicable in this case, so this is a dummy
            return Task.CompletedTask;
        }

        public async Task SearchForItem(string searchTerm)
        {
            await _searchProduct.SearchAsync(searchTerm);
        }

        public Task ValidateResultProductsAvailable(string expectedDescription, string expectedPrice)
        {
            _searchProduct.ValidateResponse(200, 299);
            _searchProduct.ValidateProductsAvailable(expectedDescription, expectedPrice);
            
            return Task.CompletedTask;
        }

        public Task ValidateResultExpectedProductCount(int expectedItems)
        {
            _searchProduct.ValidateResponse(200, 299);
            _searchProduct.ValidateProductCountExpected(expectedItems);

            return Task.CompletedTask;
        }

        public Task ValidateResultNotExpectedProductCount(int notExpectedItems)
        {
            _searchProduct.ValidateResponse(200, 299);
            _searchProduct.ValidateProductCountNotExpected(notExpectedItems);
            
            return Task.CompletedTask;
        }
    }
}
