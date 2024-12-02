using ReqnrollPlaywrightRestSharpDemo.Context;
using ReqnrollPlaywrightRestSharpDemo.UI;

namespace ReqnrollPlaywrightRestSharpDemo
{
    [Binding]
    public sealed class Hooks
    {
        private readonly IReqnrollOutputHelper _reqnrollOutputHelper;
        private readonly ScenarioContext _scenarioContext;
        private readonly ScenarioContextUI _scenarioContextUI;

        private BrowserInstance? _browserInstance;

        public Hooks(IReqnrollOutputHelper outputHelper, ScenarioContext scenarioContext, ScenarioContextUI scenarioContextUI)
        {
            _scenarioContextUI = scenarioContextUI;
            _scenarioContext = scenarioContext;
            _reqnrollOutputHelper = outputHelper;
        }

        [BeforeScenario("@ui")]
        public async Task BeforeScenario()
        {
            try
            {
                _scenarioContextUI.BaseUrl = "http://automationexercise.com"; //TODO: get from config file or env.variable

                _browserInstance = new BrowserInstance();
                await _browserInstance.Setup("chrome", true);   //TODO: get values from env.variable

                _scenarioContextUI.Page = _browserInstance.Page!;
            }
            catch (Exception exception)
            {
                _reqnrollOutputHelper.WriteLine(exception.Message);

                throw;
            }
        }

        [AfterScenario("@ui")]
        public async Task AfterScenario(ScenarioContext scenarioContext)
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
            }
        }
    }
}