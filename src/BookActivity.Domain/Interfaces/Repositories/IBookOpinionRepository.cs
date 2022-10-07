using BookActivity.Domain.Models;
using NetDevPack.Data;

namespace BookActivity.Domain.Interfaces.Repositories
{
    public interface IBookOpinionRepository : IRepository<BookOpinion>
    {
        void Add(BookOpinion bookOpinion);
    }
}
