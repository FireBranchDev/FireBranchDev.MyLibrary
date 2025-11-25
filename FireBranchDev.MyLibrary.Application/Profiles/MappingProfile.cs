using AutoMapper;
using FireBranchDev.MyLibrary.Application.Features.Book.Queries.GetBooksList;
using FireBranchDev.MyLibrary.Application.Features.Books.Commands.CreateBook;
using FireBranchDev.MyLibrary.Application.Features.Books.Commands.UpdateBook;
using FireBranchDev.MyLibrary.Application.Features.Books.Queries.GetBook;
using FireBranchDev.MyLibrary.Application.Models.Book;
using FireBranchDev.MyLibrary.Domain.Entities;

namespace FireBranchDev.MyLibrary.Application.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Book, BookListVm>().ReverseMap();

        CreateMap<Book, CreateBookCommand>().ReverseMap();
        CreateMap<Book, CreateBookDto>().ReverseMap();

        CreateMap<Book, UpdateBookCommand>().ReverseMap();

        CreateMap<Book, BookVm>().ReverseMap();

        CreateMap<BookVm, UpdateBookDto>();

        CreateMap<UpdateBookDto, BookUpdatesPatch>();
    }
}
