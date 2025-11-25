using FireBranchDev.MyLibrary.Application.Contracts.Persistence;
using FireBranchDev.MyLibrary.Application.Models.Book;
using FireBranchDev.MyLibrary.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FireBranchDev.MyLibrary.Persistence.Repositories;

public class BookRepository(MyLibraryDbContext dbContext) : BaseRepository<Book>(dbContext), IBookRepository
{
    private readonly MyLibraryDbContext _dbContext = dbContext;

    public async Task<bool> ExistsAsync(int id)
    {
        return await _dbContext.Books.Where(b => b.Id == id).AnyAsync();
    }

    public async Task<bool> IsIsbnUniqueAsync(string isbn)
    {
        return !await _dbContext.Books.Where(c => c.Isbn == isbn).AnyAsync();
    }

    public async Task PatchAsync(int id, BookUpdatesPatch updates)
    {
        bool isUpdates = false;
        foreach (var prop in typeof(BookUpdatesPatch).GetProperties())
        {
            if (prop.GetValue(updates) is not null)
            {
                isUpdates = true;
                break;
            }
        }

        if (isUpdates)
        {
            await _dbContext.Books.Where(b => b.Id == id).ExecuteUpdateAsync(setters =>
            {
                if (updates.Title is not null)
                {
                    setters.SetProperty(b => b.Title, updates.Title);
                }

                if (updates.Blurb is not null)
                {
                    setters.SetProperty(b => b.Blurb, updates.Blurb);
                }

                if (updates.AuthorFirstName is not null)
                {
                    setters.SetProperty(b => b.AuthorFirstName, updates.AuthorFirstName);
                }

                if (updates.AuthorLastName is not null)
                {
                    setters.SetProperty(b => b.AuthorLastName, updates.AuthorLastName);
                }

                if (updates.Genre is not null)
                {
                    setters.SetProperty(b => b.Genre, updates.Genre);
                }

                setters.SetProperty(b => b.Updated, DateTime.Now);
            });
        }
    }
}
