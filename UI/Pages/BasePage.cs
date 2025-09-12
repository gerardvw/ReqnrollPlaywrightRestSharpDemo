using Microsoft.Playwright;

namespace ReqnrollPlaywrightRestSharpDemo.UI.Pages
{
    public abstract class BasePage(UIDriver uiDriver)
    {
        protected UIDriver UIDriver { get; set; } = uiDriver;

        protected abstract string RelativeUri { get; }

        public async Task Navigate(int expectedStatusCodeMinimum, int expectedStatusCodeMaximum)
        {
            var response = await UIDriver.Navigate(RelativeUri);

            response.Should().NotBeNull();
            response!.Status.Should().BeInRange(expectedStatusCodeMinimum, expectedStatusCodeMaximum);
        }

        public async Task AcceptConsentIfVisible()
        {
            var consentButton = UIDriver.Page.GetByRole(AriaRole.Button, new PageGetByRoleOptions() { Name = "Consent" });
            if (await consentButton.IsVisibleAsync())
            {
                await consentButton.ClickAsync();
            }
        }
    }
}
