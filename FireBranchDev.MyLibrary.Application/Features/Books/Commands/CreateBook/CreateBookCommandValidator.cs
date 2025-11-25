using FireBranchDev.MyLibrary.Application.Contracts.Persistence;
using FluentValidation;

namespace FireBranchDev.MyLibrary.Application.Features.Books.Commands.CreateBook;

public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
{
    private readonly IBookRepository _bookRepository;

    public CreateBookCommandValidator(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;

        RuleFor(p => p.Isbn)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .MaximumLength(13).WithMessage("{PropertyName} must not exceed 13 characters")
            .MustAsync(IsIsbnUniqueAsync)
            .WithMessage("A book is already created with this {PropertyName}.");

        RuleFor(p => p.Title)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");

        RuleFor(p => p.Blurb)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .MaximumLength(250).WithMessage("{PropertyName} must not exceed 250 characters.");

        RuleFor(p => p.AuthorFirstName)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

        RuleFor(p => p.AuthorLastName)
         .NotEmpty().WithMessage("{PropertyName} is required.")
         .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

        RuleFor(p => p.Genre)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .MaximumLength(28).WithMessage("{PropertyName} must not exceed 28 characters.");
    }

    private async Task<bool> IsIsbnUniqueAsync(string isbn, CancellationToken cancellationToken)
    {
        return await _bookRepository.IsIsbnUniqueAsync(isbn);
    }
}
