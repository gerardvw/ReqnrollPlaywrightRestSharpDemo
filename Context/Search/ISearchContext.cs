namespace ReqnrollPlaywrightRestSharpDemo.Context.Search
{
    public interface ISearchContext
    {
        Task AuthenticateUser(string user);
        Task SearchForItem(string searchTerm);
        Task ValidateResultProductsAvailable(string expectedDescription, string expectedPrice);
        Task ValidateResultExpectedProductCount(int expectedItems);
        Task ValidateResultNotExpectedProductCount(int notExpectedItems);
    }
}
