using AutoMapper;
using FireBranchDev.MyLibrary.Application.Contracts.Persistence;
using FireBranchDev.MyLibrary.Application.Exceptions;
using MediatR;

namespace FireBranchDev.MyLibrary.Application.Features.Books.Queries.GetBook;

public class GetBookQueryHandler(IBookRepository bookRepository, IMapper mapper) : IRequestHandler<GetBookQuery, BookVm>
{
    private readonly IBookRepository _bookRepository = bookRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<BookVm> Handle(GetBookQuery request, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.GetByIdAsync(request.Id)
            ?? throw new NotFoundException(nameof(Book), request.Id);

        return _mapper.Map<BookVm>(book);
    }
}
