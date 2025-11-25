namespace FireBranchDev.MyLibrary.Api.JsonApi.Book;

public class BookDto
{
    public string Id { get; set; } = string.Empty;
    public string Type { get; init; } = "books";
    public string Isbn { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Blurb { get; set; } = string.Empty;
    public string AuthorFirstName { get; set; } = string.Empty;
    public string AuthorLastName { get; set; } = string.Empty;
    public string Genre { get; set; } = string.Empty;
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
}
