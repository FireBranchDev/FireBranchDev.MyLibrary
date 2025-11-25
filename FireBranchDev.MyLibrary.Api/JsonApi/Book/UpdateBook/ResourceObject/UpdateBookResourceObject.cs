namespace FireBranchDev.MyLibrary.Api.JsonApi.Book.UpdateBook.ResourceObject;

public class UpdateBookResourceObject : BaseBookResourceObject
{
    public string Id { get; set; } = string.Empty;
    public UpdateBookAttributes Attributes { get; init; } = new();
}
