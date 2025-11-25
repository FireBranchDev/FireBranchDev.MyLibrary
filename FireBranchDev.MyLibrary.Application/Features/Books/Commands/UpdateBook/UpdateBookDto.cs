namespace FireBranchDev.MyLibrary.Application.Features.Books.Commands.UpdateBook;

public class UpdateBookDto
{
    public string? Title { get; set; }
    public string? Blurb { get; set; }
    public string? AuthorFirstName { get; set; }
    public string? AuthorLastName { get; set; }
    public string? Genre { get; set; }
}
