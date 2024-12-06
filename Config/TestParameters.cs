using NUnit.Framework;
using static ReqnrollPlaywrightRestSharpDemo.Config.Enums;

namespace ReqnrollPlaywrightRestSharpDemo.Config
{
    public static class TestParameters
    {
        public static string BaseUrl
        {
            get
            {
                const string parameterName = "BaseUrl";

                var parameterValue = TestContext.Parameters[parameterName]!;

                return !string.IsNullOrEmpty(parameterValue) ? parameterValue : throw new ArgumentException(parameterName, "Parameter is not set correctly.");
            }
        }

        public static string Browser
        {
            get
            {
                const string parameterName = "Browser";

                var parameterValue = TestContext.Parameters[parameterName]!;

                return !string.IsNullOrEmpty(parameterValue) ? parameterValue : throw new ArgumentException(parameterName, "Parameter is not set correctly.");
            }
        }

        public static bool Headless
        {
            get
            {
                const string parameterName = "Headless";

                var parameterValue = TestContext.Parameters[parameterName]!;

                var success = bool.TryParse(parameterValue, out var headless);

                return success ? headless : throw new ArgumentException(parameterName, "Parameter is not set correctly.");
            }
        }

        public static TestLevels TestLevel
        {
            get
            {
                const string parameterName = "TestLevel";

                var parameterValue = TestContext.Parameters[parameterName]!;

                var success = Enum.TryParse(parameterValue, true, out TestLevels testLevel);

                return success ? testLevel : throw new ArgumentException(parameterName, "Parameter is not set correctly.");
            }
        }
    }
}