using Reqnroll.BoDi;
using ReqnrollPlaywrightRestSharpDemo.API;
using ReqnrollPlaywrightRestSharpDemo.Config;
using ReqnrollPlaywrightRestSharpDemo.Context.Search;
using ReqnrollPlaywrightRestSharpDemo.UI;
using static ReqnrollPlaywrightRestSharpDemo.Config.Enums;

namespace ReqnrollPlaywrightRestSharpDemo
{
    [Binding]
    public sealed partial class Hooks(IObjectContainer objectContainer, IReqnrollOutputHelper reqnrollOutputHelper, ScenarioContext scenarioContext)
    {
        private UIDriver? _uiDriver;
        private APIDriver? _apiDriver;

        [BeforeScenario("@ui")]
        public async Task BeforeScenarioUI()
        {
            if (TestParameters.TestLevel == TestLevels.ui) //Prevent to setup UIDriver in case of testrun is being executed on api level AND scenario has both @ui AND @api tags
            {
                try
                {
                    _uiDriver = new UIDriver(TestParameters.BaseUrl);

                    await _uiDriver.Setup(TestParameters.Browser, TestParameters.Headless);

                    //Register all contexts so they can be used in stepdefinitions
                    objectContainer.RegisterInstanceAs<ISearchContext>(new SearchContextUI(_uiDriver));
                }
                catch (Exception exception)
                {
                    reqnrollOutputHelper.WriteLine(exception.Message);

                    throw;
                }
            }
        }

        [AfterScenario("@ui")]
        public async Task AfterScenarioUI()
        {
            if (TestParameters.TestLevel == TestLevels.ui) //Prevent to teardown UIDriver in case of testrun is being executed on api level AND scenario has both @ui AND @api tags
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
        }

        [BeforeScenario("@api")]
        public async Task BeforeScenarioAPI()
        {
            if (TestParameters.TestLevel == TestLevels.api) //Prevent to setup APIDriver in case of testrun is being executed on ui level AND scenario has both @ui AND @api tags
            {
                try
                {
                    _apiDriver = new APIDriver(TestParameters.BaseUrl);

                    await _apiDriver.Setup();

                    //Register all contexts so they can be used in stepdefinitions
                    objectContainer.RegisterInstanceAs<ISearchContext>(new SearchContextAPI(_apiDriver));
                }
                catch (Exception exception)
                {
                    reqnrollOutputHelper.WriteLine(exception.Message);

                    throw;
                }
            }
        }

        [AfterScenario("@api")]
        public async Task AfterScenarioAPI()
        {
            if (TestParameters.TestLevel == TestLevels.api) //Prevent to teardown APIDriver in case of testrun is being executed on ui level AND scenario has both @ui AND @api tags
            {
                try
                {
                    if (scenarioContext.TestError != null)
                    {
                        var error = scenarioContext.TestError;
                        var testName = scenarioContext.ScenarioInfo.Title;
                        //Do some actions over here if needed, e.g. extra logging
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
}