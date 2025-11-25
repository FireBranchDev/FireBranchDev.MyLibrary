using FireBranchDev.MyLibrary.Api.JsonApi.Book.CreateBook.ResourceObject;

namespace FireBranchDev.MyLibrary.Api.JsonApi.Book.CreateBook;

public class CreateBookDto
{
    public CreateBookResourceObject Data { get; init; } = new();
}
