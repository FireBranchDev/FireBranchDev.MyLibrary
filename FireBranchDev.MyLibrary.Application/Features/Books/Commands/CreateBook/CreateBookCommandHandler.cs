using AutoMapper;
using FireBranchDev.MyLibrary.Application.Contracts.Persistence;
using MediatR;

namespace FireBranchDev.MyLibrary.Application.Features.Books.Commands.CreateBook;

public class CreateBookCommandHandler(IBookRepository bookRepository, IMapper mapper) : IRequestHandler<CreateBookCommand, CreateBookCommandResponse>
{
    private readonly IBookRepository _bookRepository = bookRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<CreateBookCommandResponse> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        var theBook = _mapper.Map<Domain.Entities.Book>(request);
        var createBookCommandResponse = new CreateBookCommandResponse();
        var validator = new CreateBookCommandValidator(_bookRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (validationResult.Errors.Count > 0)
        {
            createBookCommandResponse.IsSuccess = false;
            createBookCommandResponse.ValidationErrors = [];
            foreach (var error in validationResult.Errors)
            {
                createBookCommandResponse.ValidationErrors.Add(error.ErrorMessage);
            }
        }

        if (createBookCommandResponse.IsSuccess)
        {
            theBook = await _bookRepository.AddAsync(theBook);
            createBookCommandResponse.BookDto = _mapper.Map<CreateBookDto>(theBook);
        }

        return createBookCommandResponse;
    }
}
