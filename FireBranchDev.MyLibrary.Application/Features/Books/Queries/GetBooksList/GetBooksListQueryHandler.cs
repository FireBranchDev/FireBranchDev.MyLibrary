using AutoMapper;
using FireBranchDev.MyLibrary.Application.Contracts.Persistence;
using MediatR;

namespace FireBranchDev.MyLibrary.Application.Features.Book.Queries.GetBooksList;

public class GetBooksListQueryHandler(IMapper mapper, IAsyncRepository<Domain.Entities.Book> bookRepository) : IRequestHandler<GetBooksListQuery, List<BookListVm>>
{
    private readonly IMapper _mapper = mapper;
    private readonly IAsyncRepository<Domain.Entities.Book> _bookRepository = bookRepository;

    public async Task<List<BookListVm>> Handle(GetBooksListQuery request, CancellationToken cancellationToken)
    {
        var allBooks = (await _bookRepository.ListAllAsync()).OrderBy(x => x.Created);
        return _mapper.Map<List<BookListVm>>(allBooks);
    }
}
