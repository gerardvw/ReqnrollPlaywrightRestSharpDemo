using RestSharp;

namespace ReqnrollPlaywrightRestSharpDemo.API
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    public class APIDriver(string baseUrl)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    {
        public IRestClient RestClient;

        public Task Setup()
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
