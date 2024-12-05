using Reqnroll.BoDi;
using ReqnrollPlaywrightRestSharpDemo.API;
using ReqnrollPlaywrightRestSharpDemo.Context.Search;
using ReqnrollPlaywrightRestSharpDemo.UI;

namespace ReqnrollPlaywrightRestSharpDemo
{
    [Binding]
    public sealed class Hooks(IObjectContainer objectContainer, IReqnrollOutputHelper reqnrollOutputHelper, ScenarioContext scenarioContext)
    {
        private BrowserInstance? _browserInstance;
        private ApiClientInstance? _apiClientInstance;

        [BeforeScenario("@ui")]
        public async Task BeforeScenarioUI()
        {
            try
            {
                _browserInstance = new BrowserInstance();
                await _browserInstance.Setup("chrome", true);   //TODO: get values from env.variable

                var baseUrl = "http://automationexercise.com"; //TODO: get from config file or env.variable
                var page = _browserInstance.Page!;

                //Register all contexts so they can be used in stepdefinitions
                objectContainer.RegisterInstanceAs<ISearchContext>(new SearchContextUI(baseUrl, page));
            }
            catch (Exception exception)
            {
                reqnrollOutputHelper.WriteLine(exception.Message);

                throw;
            }
        }

        [AfterScenario("@ui")]
        public async Task AfterScenarioUI()
        {
            try
            {
                if (scenarioContext.TestError != null)
                {
                    var error = scenarioContext.TestError;
                    var testName = scenarioContext.ScenarioInfo.Title;
                    _browserInstance?.CreateScreenShotInReportFolder(testName);
                }
            }
            catch (Exception exception)
            {
                reqnrollOutputHelper.WriteLine(exception.Message);

                throw;
            }
            finally
            {
                if (_browserInstance != null)
                {
                    await _browserInstance.Teardown();
                }
                if (objectContainer.IsRegistered<ISearchContext>())
                {
                    var searchContext = objectContainer.Resolve<ISearchContext>();
                    //Do some actions over here if needed, e.g. clean up
                }
            }
        }

        [BeforeScenario("@api")]
        public async Task BeforeScenarioAPI()
        {
            try
            {
                var baseUrl = "http://automationexercise.com"; //TODO: get from config file or env.variable
                var apiUrl = $"{baseUrl}/api";

                _apiClientInstance = new ApiClientInstance();
                await _apiClientInstance.Setup(apiUrl);

                var restClient = _apiClientInstance.RestClient!;

                //Register all contexts so they can be used in stepdefinitions
                objectContainer.RegisterInstanceAs<ISearchContext>(new SearchContextAPI(restClient));
            }
            catch (Exception exception)
            {
                reqnrollOutputHelper.WriteLine(exception.Message);

                throw;
            }
        }

        [AfterScenario("@api")]
        public async Task AfterScenarioAPI()
        {
            try
            {
                if (scenarioContext.TestError != null)
                {
                    var error = scenarioContext.TestError;
                    var testName = scenarioContext.ScenarioInfo.Title;
                    //TODO: add extra logging?
                }
            }
            catch (Exception exception)
            {
                reqnrollOutputHelper.WriteLine(exception.Message);

                throw;
            }
            finally
            {
                if (_apiClientInstance != null)
                {
                    await _apiClientInstance.Teardown();
                }
                if (objectContainer.IsRegistered<ISearchContext>())
                {
                    var searchContext = objectContainer.Resolve<ISearchContext>();
                    //Do some actions over here if needed, e.g. clean up
                }
            }
        }
    }
}