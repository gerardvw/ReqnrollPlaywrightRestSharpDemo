using Microsoft.Playwright;
using NUnit.Framework;

namespace ReqnrollPlaywrightRestSharpDemo.Support
{
    public class Tracing(IBrowserContext browserContext, string logUIScenarioPrefix)
    {
        public async Task StartTracing()
        {
            if (browserContext != null)
            {
                Logger.WriteLineToLog($"{logUIScenarioPrefix} Start tracing");
                await browserContext.Tracing.StartAsync(new TracingStartOptions
                {
                    Title = TestContext.CurrentContext.Test.MethodName,
                    Screenshots = false,    //Creating screenshots can impact resources heavily, so only enable this when really needed!
                    Snapshots = true,
                    Sources = true,
                });
            }
            else
            {
                Logger.WriteLineToLog($"{logUIScenarioPrefix} Tracing not started because context is not created yet");
            }
        }

        public async Task StopTracing(bool scenarioFailed)
        {
            if (browserContext != null)
            {
                string? outputFolderAndFile = null;
                if (scenarioFailed)
                {
                    var traceFile = $"{TestContext.CurrentContext.Test.MethodName}-{Guid.NewGuid()}.zip";
                    outputFolderAndFile = Path.Combine(TracingFolder(), traceFile);
                    Logger.WriteLineToLog($"{logUIScenarioPrefix} Stop tracing and save to:{outputFolderAndFile}");
                }
                else
                {
                    Logger.WriteLineToLog($"{logUIScenarioPrefix} Stop tracing but don't save because scenario has not failed");
                }

                await browserContext.Tracing.StopAsync(new TracingStopOptions
                {
                    Path = outputFolderAndFile
                });

                if (!string.IsNullOrEmpty(outputFolderAndFile))
                {
                    Logger.WriteLineToLog($"{logUIScenarioPrefix} Adding trace file to test context:{outputFolderAndFile}");
                    TestContext.AddTestAttachment(outputFolderAndFile!, "Tracing");
                }
            }
            else
            {
                Logger.WriteLineToLog($"{logUIScenarioPrefix} Tracing not stopped because context is not created yet.");
            }
        }

        private static string TracingFolder()
        {
            var buildProjectRootFilePath = Environment.CurrentDirectory;
            return Path.Combine(buildProjectRootFilePath, "tracing");
        }
    }
}
