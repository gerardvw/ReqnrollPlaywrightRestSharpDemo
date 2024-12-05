using ReqnrollPlaywrightRestSharpDemo.API.Dtos.Search;
using RestSharp;

namespace ReqnrollPlaywrightRestSharpDemo.API.Clients
{
    public class SearchProduct(IRestClient restClient)
    {
        public Task<RestResponse<SearchProducts>> SearchAsync(string searchTerm)
        {
            var request = new RestRequest("api/searchProduct", Method.Post);

            request.AddParameter("search_product", searchTerm);

            return restClient.ExecuteAsync<SearchProducts>(request);
        }
    }
}
