using RestSharp;

namespace ReqnrollPlaywrightRestSharpDemo.API
{
    public class APIDriver
    {
        public IRestClient? RestClient;

        public Task Setup(string baseUrl)
        {
            var restClientOptions = new RestClientOptions(baseUrl) { Timeout = new TimeSpan(0, 0, 30)};
            RestClient = new RestClient(restClientOptions);

            return Task.CompletedTask;
        }

        public Task Teardown()
        {
            if (RestClient != null)
            {
                RestClient.Dispose();
            }

            return Task.CompletedTask;
        }
    }
}
