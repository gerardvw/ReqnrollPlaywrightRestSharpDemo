using Reqnroll.BoDi;
using ReqnrollPlaywrightRestSharpDemo.API;
using ReqnrollPlaywrightRestSharpDemo.Config;
using ReqnrollPlaywrightRestSharpDemo.Context.Search;
using ReqnrollPlaywrightRestSharpDemo.Support.Extensions;
using ReqnrollPlaywrightRestSharpDemo.UI;
using static ReqnrollPlaywrightRestSharpDemo.Config.Enums;

namespace ReqnrollPlaywrightRestSharpDemo
{
    [Binding]
    public sealed partial class Hooks(IObjectContainer objectContainer, ScenarioContext scenarioContext)
    {
        private UIDriver? _uiDriver;
        private APIDriver? _apiDriver;

        [BeforeScenario("@ui")]
        public async Task BeforeScenarioUI()
        {
            if (TestParameters.TestLevel == TestLevels.ui) //Prevent to setup UIDriver in case of testrun is being executed on api level AND scenario has both @ui AND @api tags
            {
                _uiDriver = new UIDriver(TestParameters.BaseUrl);

                await _uiDriver.Setup(TestParameters.Browser, TestParameters.Headless);

                //Register all contexts so they can be used in stepdefinitions
                objectContainer.RegisterInstanceAs<ISearchContext>(new SearchContextUI(_uiDriver));
            }
        }

        [AfterScenario("@ui")]
        public async Task AfterScenarioUI()
        {
            if (TestParameters.TestLevel == TestLevels.ui) //Prevent to teardown UIDriver in case of testrun is being executed on api level AND scenario has both @ui AND @api tags
            {
                await _uiDriver?.Teardown(scenarioContext.TestError != null).ForAwait()!;
                    
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
            if (TestParameters.TestLevel == TestLevels.api) //Prevent to setup APIDriver in case of testrun is being executed on ui level AND scenario has both @ui AND @api tags
            {
                _apiDriver = new APIDriver(TestParameters.BaseUrl);

                await _apiDriver.Setup();

                //Register all contexts so they can be used in stepdefinitions
                objectContainer.RegisterInstanceAs<ISearchContext>(new SearchContextAPI(_apiDriver));
            }
        }

        [AfterScenario("@api")]
        public async Task AfterScenarioAPI()
        {
            if (TestParameters.TestLevel == TestLevels.api) //Prevent to teardown APIDriver in case of testrun is being executed on ui level AND scenario has both @ui AND @api tags
            {
                await _apiDriver?.Teardown(scenarioContext).ForAwait()!;

                if (objectContainer.IsRegistered<ISearchContext>())
                {
                    var searchContext = objectContainer.Resolve<ISearchContext>();
                    //Do some actions over here if needed, e.g. clean up
                }
            }
        }
    }
}