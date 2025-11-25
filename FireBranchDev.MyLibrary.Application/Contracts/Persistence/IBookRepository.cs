using FireBranchDev.MyLibrary.Application.Models.Book;
using FireBranchDev.MyLibrary.Domain.Entities;

namespace FireBranchDev.MyLibrary.Application.Contracts.Persistence;

public interface IBookRepository : IAsyncRepository<Book>
{
    public Task<bool> IsIsbnUniqueAsync(string isbn);
    public Task PatchAsync(int id, BookUpdatesPatch updates);
    public Task<bool> ExistsAsync(int id);
}
