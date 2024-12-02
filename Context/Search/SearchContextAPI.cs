﻿using ReqnrollPlaywrightRestSharpDemo.API.Dtos.Search;
using RestSharp;

namespace ReqnrollPlaywrightRestSharpDemo.Context.Search
{
    public class SearchContextAPI(IRestClient restClient) : ISearchContext
    {
        private RestResponse<SearchProducts>? _response;

        public Task AuthenticateUser()
        {
            //No authentication and authorisation is applicable in this case, so this is a dummy
            return Task.CompletedTask;
        }

        public async Task SearchForItem(string searchTerm)
        {
            //TODO: refactor to apiclient
            var request = new RestRequest("api/searchProduct", Method.Post);

            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("search_product", searchTerm);

            _response = await restClient.ExecuteAsync<SearchProducts>(request);

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
