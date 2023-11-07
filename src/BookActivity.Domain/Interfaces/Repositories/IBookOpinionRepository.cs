using BookActivity.Domain.Models;

namespace BookActivity.Domain.Interfaces.Repositories
{
    public interface IBookOpinionRepository : IRepository<BookOpinion>
    {
        void Add(BookOpinion bookOpinion);
    }
}
