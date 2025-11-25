using AutoMapper;
using FireBranchDev.MyLibrary.Application.Contracts.Persistence;
using FireBranchDev.MyLibrary.Application.Exceptions;
using FireBranchDev.MyLibrary.Application.Models.Book;
using MediatR;

namespace FireBranchDev.MyLibrary.Application.Features.Books.Commands.UpdateBook;

public class UpdateBookCommandHandler(IBookRepository bookRepository, IMapper mapper) : IRequestHandler<UpdateBookCommand, UpdateBookCommandResponse>
{
    private readonly IBookRepository _bookRepository = bookRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<UpdateBookCommandResponse> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        bool doesBookExist = await _bookRepository.ExistsAsync(request.Id);
        if (!doesBookExist) throw new NotFoundException(nameof(Book), request.Id);

        var validator = new UpdateBookCommandValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        var updateBookCommandResponse = new UpdateBookCommandResponse();

        if (validationResult.Errors.Count > 0)
        {
            updateBookCommandResponse.IsSuccess = false;
            updateBookCommandResponse.ValidationErrors = [];
            foreach (var error in validationResult.Errors)
            {
                updateBookCommandResponse.ValidationErrors.Add(error.ErrorMessage);
            }
        }

        if (updateBookCommandResponse.IsSuccess)
        {
            await _bookRepository.PatchAsync(request.Id, _mapper.Map<BookUpdatesPatch>(request.BookUpdates));
        }

        return updateBookCommandResponse;
    }
}
