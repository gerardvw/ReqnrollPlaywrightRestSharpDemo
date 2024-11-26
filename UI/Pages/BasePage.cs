using Microsoft.Playwright;

namespace ReqnrollPlaywrightRestSharpDemo.UI.Pages
{
    public abstract class BasePage(string baseUrl, IPage page)
    {
        protected string BaseUrl { get; set; } = baseUrl;
        protected IPage Page { get; set; } = page;

        protected abstract string RelativeUri { get; }

        public Task<IResponse?> Navigate()
        {
            return Page.GotoAsync($"{BaseUrl.TrimEnd('/')}{RelativeUri}");
        }

        public async Task AcceptConsentIfVisible()
        {
            var consentButton = Page.GetByRole(AriaRole.Button, new PageGetByRoleOptions() { Name = "Consent" });
            if (await consentButton.IsVisibleAsync())
            {
                await consentButton.ClickAsync();
            }
        }
    }
}
