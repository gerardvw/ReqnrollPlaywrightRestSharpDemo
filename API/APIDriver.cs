using RestSharp;

namespace ReqnrollPlaywrightRestSharpDemo.API
{
    public class APIDriver(string baseUrl)
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public IRestClient RestClient;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        public Task Setup()
        {
            var restClientOptions = new RestClientOptions(baseUrl) { Timeout = new TimeSpan(0, 0, 30)};
            RestClient = new RestClient(restClientOptions);

            return Task.CompletedTask;
        }

        public Task Teardown(ScenarioContext scenarioContext)
        {
            HandleScenarioFailureAndTracing(scenarioContext);

            RestClient?.Dispose();

            return Task.CompletedTask;
        }

        private static Task HandleScenarioFailureAndTracing(ScenarioContext scenarioContext)
        {
            if (scenarioContext.TestError != null)
            {
                var error = scenarioContext.TestError;
                var testName = scenarioContext.ScenarioInfo.Title;
                //Do some actions over here if needed, e.g. extra logging
            }

            return Task.CompletedTask;
        }
    }
}
