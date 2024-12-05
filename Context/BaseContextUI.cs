using ReqnrollPlaywrightRestSharpDemo.UI;
using System.Diagnostics.CodeAnalysis;

namespace ReqnrollPlaywrightRestSharpDemo.Context
{
    [method: SetsRequiredMembers]
    public class BaseContextUI(UIDriver uiDriver)
    {
        public required UIDriver UIDriver { get; set; } = uiDriver;
    }
}
