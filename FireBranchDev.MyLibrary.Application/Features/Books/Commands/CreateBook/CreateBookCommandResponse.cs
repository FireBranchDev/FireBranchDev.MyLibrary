using FireBranchDev.MyLibrary.Application.Responses;
namespace FireBranchDev.MyLibrary.Application.Features.Books.Commands.CreateBook;

public class CreateBookCommandResponse : BaseResponse
{
    public CreateBookCommandResponse() : base()
    {
    }

    public CreateBookDto? BookDto { get; set; }
}
