using BookActivity.Domain.Models;
using NetDevPack.Data;

namespace BookActivity.Domain.Interfaces.Repositories
{
    public interface IBookNoteRepository : IRepository<BookNote>
    {
        void Add(BookNote bookNote);
        void Remove(BookNote bookNote);
    }
}
