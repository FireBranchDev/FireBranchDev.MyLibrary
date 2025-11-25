using FireBranchDev.MyLibrary.Api.JsonApi.Book.UpdateBook.ResourceObject;

namespace FireBranchDev.MyLibrary.Api.JsonApi.Book.UpdateBook;

public class UpdateBookDto
{
    public UpdateBookResourceObject Data { get; init; } = new();
}
