using Reqnroll.BoDi;
using ReqnrollPlaywrightRestSharpDemo.Context.Search;
using ReqnrollPlaywrightRestSharpDemo.UI;

namespace ReqnrollPlaywrightRestSharpDemo
{
    [Binding]
    public sealed class Hooks
    {
        private readonly IReqnrollOutputHelper _reqnrollOutputHelper;
        private readonly ScenarioContext _scenarioContext;

        private BrowserInstance? _browserInstance;

        private readonly IObjectContainer _objectContainer;

        public Hooks(IObjectContainer objectContainer, IReqnrollOutputHelper reqnrollOutputHelper, ScenarioContext scenarioContext)
        {
            _objectContainer = objectContainer;
            _scenarioContext = scenarioContext;
            _reqnrollOutputHelper = reqnrollOutputHelper;
        }

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
                _objectContainer.RegisterInstanceAs<ISearchContext>(new SearchContextUI(baseUrl, page));
            }
            catch (Exception exception)
            {
                _reqnrollOutputHelper.WriteLine(exception.Message);

                throw;
            }
        }

        [AfterScenario("@ui")]
        public async Task AfterScenarioUI()
        {
            try
            {
                if (_scenarioContext.TestError != null)
                {
                    var error = _scenarioContext.TestError;
                    var testName = _scenarioContext.ScenarioInfo.Title;
                    _browserInstance?.CreateScreenShotInReportFolder(testName);
                }
            }
            catch (Exception exception)
            {
                _reqnrollOutputHelper.WriteLine(exception.Message);

                throw;
            }
            finally
            {
                if (_browserInstance != null)
                {
                    await _browserInstance.Teardown();
                }
                if (_objectContainer.IsRegistered<ISearchContext>())
                {
                    var searchContext = _objectContainer.Resolve<ISearchContext>();
                    //Do some actions over here if needed, e.g. clean up
                }
            }
        }
    }
}