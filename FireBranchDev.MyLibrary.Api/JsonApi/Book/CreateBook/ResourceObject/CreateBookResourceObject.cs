namespace FireBranchDev.MyLibrary.Api.JsonApi.Book.CreateBook.ResourceObject;

public class CreateBookResourceObject : BaseBookResourceObject
{
    public string? Id { get; set; }
    public CreateBookAttributes Attributes { get; init; } = new();
}
