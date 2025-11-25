using MediatR;

namespace FireBranchDev.MyLibrary.Application.Features.Books.Commands.UpdateBook;

public class UpdateBookCommand : IRequest<UpdateBookCommandResponse>
{
    public int Id { get; set; }
    public UpdateBookDto BookUpdates { get; set; } = new();
}
