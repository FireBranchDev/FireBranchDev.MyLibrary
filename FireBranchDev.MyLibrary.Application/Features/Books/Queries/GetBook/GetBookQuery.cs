using MediatR;

namespace FireBranchDev.MyLibrary.Application.Features.Books.Queries.GetBook;

public class GetBookQuery : IRequest<BookVm>
{
    public int Id { get; set; }
}
