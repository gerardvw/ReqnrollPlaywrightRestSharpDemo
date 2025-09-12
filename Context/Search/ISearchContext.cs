namespace ReqnrollPlaywrightRestSharpDemo.Context.Search
{
    public interface ISearchContext
    {
        Task AuthenticateUser(string user);
        Task SearchForItem(string searchTerm);
        Task ValidateResult(string expectedDescription, string expectedPrice);
        Task ValidateResultExpected(int expectedItems);
        Task ValidateResultNotExpected(int notExpectedItems);
    }
}
