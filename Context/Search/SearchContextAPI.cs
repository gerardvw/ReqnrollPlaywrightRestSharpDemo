using ReqnrollPlaywrightRestSharpDemo.API;
using ReqnrollPlaywrightRestSharpDemo.API.Clients;
using ReqnrollPlaywrightRestSharpDemo.API.Dtos.Search;
using RestSharp;

namespace ReqnrollPlaywrightRestSharpDemo.Context.Search
{
    public class SearchContextAPI(APIDriver apiDriver) : ISearchContext
    {
        private RestResponse<SearchProducts>? _response;

        public Task AuthenticateUser()
        {
            //No authentication and authorisation is applicable in this case, so this is a dummy
            return Task.CompletedTask;
        }

        public async Task SearchForItem(string searchTerm)
        {
            var searchProduct = new SearchProduct(apiDriver.RestClient);
            _response = await searchProduct.SearchAsync(searchTerm);

            _response.Should().NotBeNull();
            ((int)_response.StatusCode).Should().BeInRange(200, 299);
        }

        public Task ValidateResult(string expectedDescription, string expectedPrice)
        {
            _response?.Data?.Products.Should().Contain(p => p.Name == expectedDescription && p.Price == expectedPrice);

            return Task.CompletedTask;
        }

        public Task ValidateResultExpected(int expectedItems)
        {
            _response?.Data?.Products.Count.Should().Be(expectedItems);

            return Task.CompletedTask;
        }

        public Task ValidateResultNotExpected(int notExpectedItems)
        {
            _response?.Data?.Products.Count.Should().NotBe(notExpectedItems);

            return Task.CompletedTask;
        }
    }
}
