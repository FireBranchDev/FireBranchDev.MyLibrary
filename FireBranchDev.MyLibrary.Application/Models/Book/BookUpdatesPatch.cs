namespace FireBranchDev.MyLibrary.Application.Models.Book;

public class BookUpdatesPatch
{
    public string? Title { get; set; }
    public string? Blurb { get; set; }
    public string? AuthorFirstName { get; set; }
    public string? AuthorLastName { get; set; }
    public string? Genre { get; set; }
}
