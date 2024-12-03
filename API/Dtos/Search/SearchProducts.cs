namespace ReqnrollPlaywrightRestSharpDemo.API.Dtos.Search
{
    public record SearchProducts(int ResponseCode, List<Product> Products) { }

    public record Product(int Id, string Name, string Price, string Brand, Category Category) { }

#pragma warning disable IDE1006 // Naming Styles
    public record Category(UserType Usertype, string category) { }
#pragma warning restore IDE1006 // Naming Styles

    public record UserType(string Usertype) { }
}
