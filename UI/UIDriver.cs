using Microsoft.Playwright;
using NUnit.Framework;

namespace ReqnrollPlaywrightRestSharpDemo.UI
{
    public class UIDriver
    {
        private IBrowser? _browser;
        private IBrowserContext? _context;

        public IPage? Page;

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

            _context = await _browser.NewContextAsync(new BrowserNewContextOptions { IgnoreHTTPSErrors = true, ViewportSize = new ViewportSize { Width = 1920, Height = 1080 }, ScreenSize = new ScreenSize { Width = 1920, Height = 1080 }, HttpCredentials = new HttpCredentials { Username = "TODO", Password = "TODO" } });
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
