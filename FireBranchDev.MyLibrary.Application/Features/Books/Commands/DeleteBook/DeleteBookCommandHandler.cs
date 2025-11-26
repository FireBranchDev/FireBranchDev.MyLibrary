using FireBranchDev.MyLibrary.Application.Contracts.Persistence;
using FireBranchDev.MyLibrary.Application.Exceptions;
using MediatR;

namespace FireBranchDev.MyLibrary.Application.Features.Books.Commands.DeleteBook;

public class DeleteBookCommandHandler(IBookRepository bookRepository) : IRequestHandler<DeleteBookCommand>
{
    private readonly IBookRepository _bookRepository = bookRepository;

    public async Task Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.GetByIdAsync(request.Id) ?? throw new NotFoundException(nameof(Domain.Entities.Book), request.Id);
        await _bookRepository.DeleteAsync(book);
    }
}
