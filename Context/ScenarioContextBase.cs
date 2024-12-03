using System.Diagnostics.CodeAnalysis;

namespace ReqnrollPlaywrightRestSharpDemo.Context
{
    [method: SetsRequiredMembers]
    public class ScenarioContextBase(string baseUrl)
    {
        public required string BaseUrl { get; set; } = baseUrl;
    }
}
