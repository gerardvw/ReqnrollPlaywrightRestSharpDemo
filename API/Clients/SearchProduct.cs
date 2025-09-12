using ReqnrollPlaywrightRestSharpDemo.API.Dtos.Search;
using RestSharp;

namespace ReqnrollPlaywrightRestSharpDemo.API.Clients
{
    public class SearchProduct(IRestClient restClient)
    {
        private RestResponse<SearchProducts>? _response;

        public async Task SearchAsync(string searchTerm)
        {
            var request = new RestRequest("api/searchProduct", Method.Post);

            request.AddParameter("search_product", searchTerm);

            _response = await restClient.ExecuteAsync<SearchProducts>(request);
        }

        public Task ValidateResponse(int expectedStatusCodeMinimum, int expectedStatusCodeMaximum)
        {
            _response.Should().NotBeNull();
            ((int) _response.StatusCode).Should().BeInRange(expectedStatusCodeMinimum, expectedStatusCodeMaximum);
            
            return Task.CompletedTask;
        }

        public Task ValidateProductAvailable(string expectedDescription, string expectedPrice)
        {
            _response?.Data?.Products.Should().Contain(p => p.Name == expectedDescription && p.Price == expectedPrice);

            return Task.CompletedTask;
        }

        public Task ValidateProductCountExpected(int expectedItems)
        {
            _response?.Data?.Products.Count.Should().Be(expectedItems);

            return Task.CompletedTask;
        }

        public Task ValidateProductCountNotExpected(int notExpectedItems)
        {
            _response?.Data?.Products.Count.Should().NotBe(notExpectedItems);

            return Task.CompletedTask;
        }
    }
}
