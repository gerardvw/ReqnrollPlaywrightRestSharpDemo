using Microsoft.Playwright;
using System.Diagnostics.CodeAnalysis;

namespace ReqnrollPlaywrightRestSharpDemo.Context
{
    [method:SetsRequiredMembers]
    public class ScenarioContextUI(string baseUrl, IPage page) : ScenarioContextBase(baseUrl)
    {
        public required IPage Page { get; set; } = page;
    }
}
