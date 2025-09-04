using System.Runtime.CompilerServices;
using NUnit.Framework;

namespace ReqnrollPlaywrightRestSharpDemo.Support
{
    public static class Logger
    {
        private const string DateTimeFormat = "dd-MM-yyyy hh:mm:ss:ffff";

        /// <summary>
        /// Write line to full log only
        /// </summary>
        public static void WriteLineToLog(string message, [CallerMemberName] string memberName = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0)
        {
            string className = Path.GetFileNameWithoutExtension(filePath);

            TestContext.Progress.WriteLine($"[{DateTime.Now.ToString(DateTimeFormat)}] {className}.{memberName}:{lineNumber} {message}");
        }

        /// <summary>
        /// Write line to full log and to report
        /// </summary>
        public static void WriteLineToLogAndReport(string message, [CallerMemberName] string memberName = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0)
        {
            string className = Path.GetFileNameWithoutExtension(filePath);

            TestContext.Out.WriteLine($"[{DateTime.Now.ToString(DateTimeFormat)}] {className}.{memberName}:{lineNumber} {message}");
        }
    }
}
