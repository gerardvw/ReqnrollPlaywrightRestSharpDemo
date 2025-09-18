using Microsoft.Playwright;
using NUnit.Framework;

namespace ReqnrollPlaywrightRestSharpDemo.Support
{
    public class Tracing(IBrowserContext browserContext)
    {
        private readonly IBrowserContext? browserContext = browserContext ?? throw new ArgumentNullException(nameof(browserContext));
        private static string LogUIScenarioPrefix => !string.IsNullOrEmpty(TestContext.CurrentContext?.Test?.MethodName) ? $"UI Scenario:{TestContext.CurrentContext?.Test?.MethodName}." : "UI Scenario:<none>.";

        public async Task StartTracing()
        {
            Logger.WriteLineToLog($"{LogUIScenarioPrefix} Start tracing");
            await browserContext!.Tracing.StartAsync(new TracingStartOptions
            {
                Title = TestContext.CurrentContext.Test.MethodName,
                Screenshots = false,    //Creating screenshots can impact resources heavily, so only enable this when really needed!
                Snapshots = true,
                Sources = true,
            });
        }

        public async Task StopTracing(bool scenarioFailed)
        {
            string? outputFolderAndFile = null;
            if (scenarioFailed)
            {
                var traceFile = $"{TestContext.CurrentContext.Test.MethodName}-{Guid.NewGuid()}.zip";
                outputFolderAndFile = Path.Combine(TracingFolder(), traceFile);
                Logger.WriteLineToLog($"{LogUIScenarioPrefix} Stop tracing and save to:{outputFolderAndFile}");
            }
            else
            {
                Logger.WriteLineToLog($"{LogUIScenarioPrefix} Stop tracing but don't save because scenario has not failed");
            }

            await browserContext!.Tracing.StopAsync(new TracingStopOptions
            {
                Path = outputFolderAndFile
            });

            if (!string.IsNullOrEmpty(outputFolderAndFile))
            {
                Logger.WriteLineToLog($"{LogUIScenarioPrefix} Adding trace file to test context:{outputFolderAndFile}");
                TestContext.AddTestAttachment(outputFolderAndFile!, "Tracing");
            }
        }

        private static string TracingFolder()
        {
            var buildProjectRootFilePath = Environment.CurrentDirectory;
            return Path.Combine(buildProjectRootFilePath, "tracing");
        }
    }
}
