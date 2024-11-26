using Microsoft.Playwright;

namespace ReqnrollPlaywrightRestSharpDemo.Context
{
    public class ScenarioContextUI : ScenarioContextBase
    {
        public IPage? Page { get; set; }
    }
}
