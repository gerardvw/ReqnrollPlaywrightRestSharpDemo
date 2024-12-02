using Microsoft.Playwright;

namespace ReqnrollPlaywrightRestSharpDemo.Context
{
    public class ScenarioContextUI : ScenarioContextBase
    {
        public required IPage Page { get; set; }
    }
}
