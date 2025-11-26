using AutoMapper;
using FireBranchDev.MyLibrary.Api.JsonApi.Book;
using FireBranchDev.MyLibrary.Application.Exceptions;
using FireBranchDev.MyLibrary.Application.Features.Book.Queries.GetBooksList;
using FireBranchDev.MyLibrary.Application.Features.Books.Commands.CreateBook;
using FireBranchDev.MyLibrary.Application.Features.Books.Commands.DeleteBook;
using FireBranchDev.MyLibrary.Application.Features.Books.Commands.UpdateBook;
using FireBranchDev.MyLibrary.Application.Features.Books.Queries.GetBook;
using JsonApiSerializer.JsonApi;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CreateBookDto = FireBranchDev.MyLibrary.Api.JsonApi.Book.CreateBook.CreateBookDto;
using UpdateBookDto = FireBranchDev.MyLibrary.Api.JsonApi.Book.UpdateBook.UpdateBookDto;

namespace FireBranchDev.MyLibrary.Api.Controllers;

[Route("api/[controller]")]
public class BooksController(IMediator mediator, IMapper mapper) : JsonApiControllerBase
{
    private readonly IMediator _mediator = mediator;
    private readonly IMapper _mapper = mapper;

    [HttpPost(Name = "AddBook"), Authorize(Policy = "create:book")]
    [ProducesDefaultResponseType(typeof(DocumentRoot<BookDto>))]
    [ProducesErrorResponseType(typeof(DocumentRoot<BookDto>))]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<DocumentRoot<BookDto>>> Create([FromBody] CreateBookDto book)
    {
        if (book.Data.Type != "books")
        {
            return Conflict(new List<Error>()
            {
                new()
                {
                    Detail = "Only accepting resource type of 'books'."
                }
            });
        }

        if (book.Data.Id is not null)
        {
            return Conflict(new List<Error>()
            {
                new()
                {
                    Detail = "Not accepting a client-generated ID."
                }
            });
        }

        var command = new CreateBookCommand()
        {
            Isbn = book.Data.Attributes.Isbn,
            Title = book.Data.Attributes.Title,
            Blurb = book.Data.Attributes.Blurb,
            AuthorFirstName = book.Data.Attributes.AuthorFirstName,
            AuthorLastName = book.Data.Attributes.AuthorLastName,
            Genre = book.Data.Attributes.Genre
        };

        var response = await _mediator.Send(command);

        if (response.ValidationErrors is not null && response.ValidationErrors.Count > 0)
        {
            var errors = new List<Error>();
            foreach (var validationError in response.ValidationErrors)
            {
                errors.Add(
                    new()
                    {
                        Detail = validationError,
                    }
                );
            }
            
            return BadRequest(errors);
        }

        if (response.BookDto is not null)
        {
            var result = Created();
            result.Value = new BookDto()
            {
                Id = response.BookDto.Id.ToString(),
                Isbn = response.BookDto.Isbn,
                Title = response.BookDto.Title,
                Blurb = response.BookDto.Blurb,
                AuthorFirstName = response.BookDto.AuthorFirstName,
                AuthorLastName = response.BookDto.AuthorLastName,
                Genre = response.BookDto.Genre,
                Created = response.BookDto.Created,
                Updated = response.BookDto.Updated
            };
            return result;
        }

        return NoContent();
    }

    [HttpGet(Name = "GetAllBooks")]
    [ProducesDefaultResponseType(typeof(DocumentRoot<List<BookDto>>))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<DocumentRoot<List<BookDto>>>> GetAllBooks()
    {
        var books = await _mediator.Send(new GetBooksListQuery());

        var booksDtos = new List<BookDto>();
        foreach (var book in books)
        {
            booksDtos.Add(new()
            {
                Id = book.Id.ToString(),
                Isbn = book.Isbn,
                Title = book.Title,
                Blurb = book.Blurb,
                AuthorFirstName = book.AuthorFirstName,
                AuthorLastName = book.AuthorLastName,
                Genre = book.Genre,
                Created = book.Created,
                Updated = book.Updated
            });
        }

        return Ok(booksDtos);
    }

    [HttpPatch("{id:int}", Name = "UpdateBook")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(DocumentRoot<object?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(DocumentRoot<object?>), StatusCodes.Status404NotFound, Description = "Project Not Found")]
    public async Task<ActionResult<DocumentRoot<object?>>> UpdateBook(int id, [FromBody] UpdateBookDto book)
    {
        var updateBookCommand = new UpdateBookCommand()
        {
            Id = id
        };
        updateBookCommand.BookUpdates.Title = book.Data.Attributes.Title;
        updateBookCommand.BookUpdates.Blurb = book.Data.Attributes.Blurb;
        updateBookCommand.BookUpdates.AuthorFirstName = book.Data.Attributes.AuthorFirstName;
        updateBookCommand.BookUpdates.AuthorLastName = book.Data.Attributes.AuthorLastName;
        updateBookCommand.BookUpdates.Genre = book.Data.Attributes.Genre;

        var documentRoot = DocumentRoot.Create<object?>(null);

        if (!ModelState.IsValid)
        {
            List<Error> errors = [];
            foreach (var state in ModelState)
            {
                foreach (var error in state.Value.Errors)
                {
                    errors.Add(
                        new Error
                        {
                            Detail = error.ErrorMessage
                        }
                    );
                }
            }
            
            documentRoot.Errors = errors;

            return BadRequest(documentRoot);
        }

        try
        {
            var response = await _mediator.Send(updateBookCommand);
            if (response.ValidationErrors is not null && response.ValidationErrors.Count > 0)
            {
                List<Error> errors = [];
                foreach (var validationError in response.ValidationErrors)
                {
                    errors.Add(
                        new Error
                        {
                            Detail = validationError,
                        }
                    );
                }

                documentRoot.Errors = errors;

                return BadRequest(documentRoot);
            }

            return NoContent();
        }
        catch (NotFoundException)
        {
            documentRoot.Errors = [
                new() {
                    Detail = "Project not found."
                }
            ];

            return NotFound(documentRoot);
        }
    }

    [HttpDelete("{bookId:int}", Name = "DeleteBook")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(DocumentRoot<object>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteBook(int bookId)
    {
        try
        {
            await _mediator.Send(new DeleteBookCommand
            {
                Id = bookId
            });
        }
        catch (NotFoundException)
        {
            return NotFound(new List<Error>()
            {
                new()
                {
                    Detail = "No record of a Book with the provided ID."
                }
            });
        }
       
        return NoContent();
    }

    [HttpGet("{id:int}", Name = "GetBook")]
    [ProducesDefaultResponseType(typeof(DocumentRoot<BookDto>))]
    [ProducesErrorResponseType(typeof(DocumentRoot<BookDto>))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<DocumentRoot<BookDto>>> GetBook(int id)
    {
        try
        {
            var book = await _mediator.Send(new GetBookQuery
            {
                Id = id
            });

            return DocumentRoot.Create(_mapper.Map<BookDto>(book));
        }
        catch (NotFoundException)
        {
            return NotFound(new Error()
            {
                Detail = "No record of a book with the provided ID."
            });
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new Error()
            {
                Detail = "Server encountered a internal error."
            });
        }
    }
}
