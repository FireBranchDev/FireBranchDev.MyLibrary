using MediatR;

namespace FireBranchDev.MyLibrary.Application.Features.Book.Queries.GetBooksList;

public class GetBooksListQuery : IRequest<List<BookListVm>>
{
}
