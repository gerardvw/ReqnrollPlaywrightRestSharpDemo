using Microsoft.Playwright;
using NUnit.Framework;

namespace ReqnrollPlaywrightRestSharpDemo.UI
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    public class UIDriver(string baseUrl)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    {
        private IBrowser? _browser;
        private IBrowserContext? _context;

        public string BaseUrl { get; set; } = baseUrl;
        public IPage Page;

        public async Task Setup(string browserName, bool headless)
        {
            var playwright = await Playwright.CreateAsync().ConfigureAwait(false);

            _browser = await playwright.Chromium.LaunchAsync(new()
            {
                Channel = browserName,
                Headless = headless,
                //Disable auto login (Windows Integrated Authentication) by adding auth-server-allowlist argument, so given http credentials are always applied
                Args = new List<string>() { "--auth-server-allowlist=\"_\"", "--start-maximized" }
            });

            _context = await _browser.NewContextAsync(new BrowserNewContextOptions { IgnoreHTTPSErrors = true, ViewportSize = ViewportSize.NoViewport, ScreenSize = new ScreenSize { Width = 1920, Height = 1080 }, HttpCredentials = new HttpCredentials { Username = "TODO", Password = "TODO" } });
            _context.SetDefaultTimeout(10000);
            await _context.ClearCookiesAsync();

            Page = await _context.NewPageAsync();
        }

        public async Task Teardown()
        {
            if (Page != null)
            {
                await Page.CloseAsync();
            }
            if (_context != null)
            {
                await _context.CloseAsync();
            }
            if (_browser != null)
            {
                await _browser.CloseAsync();
            }
        }

        public void CreateScreenShotInReportFolder(string step = "")
        {
            var screenshotFile = $"{TestContext.CurrentContext.Test.ClassName}-{TestContext.CurrentContext.Test.MethodName}-{step}{Guid.NewGuid()}.png";

            var outputFolderAndFile = Path.Combine(ReportFolder(), screenshotFile);

            Page?.ScreenshotAsync(new PageScreenshotOptions
            {
                Path = outputFolderAndFile,
                FullPage = true,
            }).Wait();
        }

        private string ReportFolder()
        {
            var buildProjectRootFilePath = Environment.CurrentDirectory;

            return Path.Combine(buildProjectRootFilePath, "report");
        }
    }
}
