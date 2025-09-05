using Microsoft.Playwright;
using NUnit.Framework;
using ReqnrollPlaywrightRestSharpDemo.Support;

namespace ReqnrollPlaywrightRestSharpDemo.UI
{
    public class UIDriver(string baseUrl)
    {
        private IBrowser? _browser;
        private IBrowserContext? _browserContext;
        private Tracing? _tracing;

        private static string LogUIScenarioPrefix => !string.IsNullOrEmpty(TestContext.CurrentContext?.Test?.MethodName) ? $"UI Scenario:{TestContext.CurrentContext?.Test?.MethodName}." : "UI Scenario:<none>.";

        public string BaseUrl { get; set; } = baseUrl;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public IPage Page;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        public async Task Setup(string browserName, bool headless)
        {
            await CreateBrowser(browserName, headless);
            await CreateBrowserContext();
            await BrowserContextClearCookies();

            SetDefaultTimeouts();

            await BrowserContextCreatePage();
            await CreateAndStartTracing();
        }

        private async Task CreateAndStartTracing()
        {
            ArgumentNullException.ThrowIfNull(_browserContext);

            _tracing = new Tracing(_browserContext, LogUIScenarioPrefix);
            await _tracing.StartTracing();
        }

        private async Task BrowserContextCreatePage()
        {
            ArgumentNullException.ThrowIfNull(_browserContext);

            Page = await _browserContext.NewPageAsync();
        }

        private async Task BrowserContextClearCookies()
        {
            ArgumentNullException.ThrowIfNull(_browserContext);

            await _browserContext.ClearCookiesAsync();
        }

        private void SetDefaultTimeouts()
        {
            ArgumentNullException.ThrowIfNull(_browserContext);

            _browserContext.SetDefaultTimeout(10000);
            _browserContext.SetDefaultNavigationTimeout(10000);
            Assertions.SetDefaultExpectTimeout(10000);
        }

        private async Task CreateBrowserContext()
        {
            ArgumentNullException.ThrowIfNull(_browser);

            _browserContext = await _browser.NewContextAsync(new BrowserNewContextOptions { IgnoreHTTPSErrors = true, ViewportSize = ViewportSize.NoViewport, ScreenSize = new ScreenSize { Width = 1920, Height = 1080 }, HttpCredentials = new HttpCredentials { Username = "TODO", Password = "TODO" } });
        }

        private async Task CreateBrowser(string browserName, bool headless)
        {
            var playwright = await Playwright.CreateAsync().ConfigureAwait(false);

            _browser = await playwright.Chromium.LaunchAsync(new()
            {
                Channel = browserName,
                Headless = headless,
                //Disable auto login (Windows Integrated Authentication) by adding auth-server-allowlist argument, so given http credentials are always applied
                Args = ["--auth-server-allowlist=\"_\"", "--start-maximized"]
            });
        }

        public async Task Teardown(bool scenarioFailed)
        {
            await HandleScenarioFailureAndTracing(scenarioFailed);

            if (Page != null)
            {
                await Page.CloseAsync();
            }
            if (_browserContext != null)
            {
                await _browserContext.CloseAsync();
            }
            if (_browser != null)
            {
                await _browser.CloseAsync();
            }
        }

        private async Task HandleScenarioFailureAndTracing(bool scenarioFailed)
        {
            await CreateScreenShotInReportFolderIfFailed(scenarioFailed);

            if (_tracing != null)
            {
                await _tracing.StopTracing(scenarioFailed);
            }
        }

        private async Task CreateScreenShotInReportFolderIfFailed(bool scenarioFailed, string step = "")
        {
            if (scenarioFailed)
            {
                var screenshotFile = $"{TestContext.CurrentContext.Test.ClassName}-{TestContext.CurrentContext.Test.MethodName}-{step}{Guid.NewGuid()}.png";

                var outputFolderAndFile = Path.Combine(ReportFolder(), screenshotFile);
                if (Page != null)
                {
                    await Page.ScreenshotAsync(new PageScreenshotOptions
                    {
                        Path = outputFolderAndFile,
                        FullPage = true,
                    });
                }
            }
        }

        private static string ReportFolder()
        {
            var buildProjectRootFilePath = Environment.CurrentDirectory;

            return Path.Combine(buildProjectRootFilePath, "report");
        }
    }
}
