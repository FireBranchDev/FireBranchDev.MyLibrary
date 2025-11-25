namespace FireBranchDev.MyLibrary.Api.JsonApi.Book.CreateBook.ResourceObject;

public class CreateBookAttributes
{
    public string Isbn { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Blurb { get; set; } = string.Empty;
    public string AuthorFirstName { get; set; } = string.Empty;
    public string AuthorLastName { get; set; } = string.Empty;
    public string Genre { get; set; } = string.Empty;
}
