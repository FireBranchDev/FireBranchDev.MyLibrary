using FluentValidation;

namespace FireBranchDev.MyLibrary.Application.Features.Books.Commands.UpdateBook;

public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
{
    public UpdateBookCommandValidator()
    {
        RuleFor(x => x.BookUpdates.Title)
            .MaximumLength(50).WithMessage("Title must not exceed 50 characters.");
    }
}
