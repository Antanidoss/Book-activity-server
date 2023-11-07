using BookActivity.Domain.Models;

namespace BookActivity.Domain.Interfaces.Repositories
{
    public interface IBookNoteRepository : IRepository<BookNote>
    {
        void Add(BookNote bookNote);
        void Remove(BookNote bookNote);
    }
}
