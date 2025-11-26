using FireBranchDev.MyLibrary.Api.JsonApi.Book;
using FireBranchDev.MyLibrary.Application.Features.Books.Queries.GetBook;

namespace FireBranchDev.MyLibrary.Api.JsonApi;

public class Profile : AutoMapper.Profile
{
    public Profile()
    {
        CreateMap<BookVm, BookDto>();
    }
}
