namespace ReqnrollPlaywrightRestSharpDemo.Support.Extensions
{
    public static class TaskExtensions
    {
        /// <summary>
        /// Checks task for null and returns Task.CompletedTask in that case to prevent NullReferenceException in case of an await, otherwise returns the task itself
        /// </summary>
        /// <param name="task">the task which is awaited</param>
        /// <returns>task in case of != null orelse Task.CompletedTask</returns>
        public static Task ForAwait(this Task task)
        {
            return task ?? Task.CompletedTask;
        }
    }
}
