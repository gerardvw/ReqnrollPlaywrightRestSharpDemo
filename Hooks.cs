using Reqnroll.BoDi;
using ReqnrollPlaywrightRestSharpDemo.API;
using ReqnrollPlaywrightRestSharpDemo.Context.Search;
using ReqnrollPlaywrightRestSharpDemo.UI;

namespace ReqnrollPlaywrightRestSharpDemo
{
    [Binding]
    public sealed class Hooks(IObjectContainer objectContainer, IReqnrollOutputHelper reqnrollOutputHelper, ScenarioContext scenarioContext)
    {
        private UIDriver? _uiDriver;
        private APIDriver? _apiDriver;

        [BeforeScenario("@ui")]
        public async Task BeforeScenarioUI()
        {
            try
            {
                _uiDriver = new UIDriver();
                await _uiDriver.Setup("chrome", true);   //TODO: get values from env.variable

                var baseUrl = "http://automationexercise.com"; //TODO: get from config file or env.variable
                var page = _uiDriver.Page!;

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
                    _uiDriver?.CreateScreenShotInReportFolder(testName);
                }
            }
            catch (Exception exception)
            {
                reqnrollOutputHelper.WriteLine(exception.Message);

                throw;
            }
            finally
            {
                if (_uiDriver != null)
                {
                    await _uiDriver.Teardown();
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

                _apiDriver = new APIDriver();
                await _apiDriver.Setup(apiUrl);

                var restClient = _apiDriver.RestClient!;

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
                if (_apiDriver != null)
                {
                    await _apiDriver.Teardown();
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