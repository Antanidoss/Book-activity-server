using BookActivity.Domain.Filters.Models;
using BookActivity.Domain.Interfaces;
using BookActivity.Domain.Models;
using BookActivity.Domain.Queries.AppUserQueries.GetUserProfileInfo;
using BookActivity.Domain.Queries.AppUserQueries.GetUsersByFilter;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookActivity.Domain.Filters.SelectFilterHandlers
{
    internal sealed class AppUserSelectFilterHandler :
        IFilterSelectHandler<AppUser, IEnumerable<SelectedAppUser>, GetUsersByFilterQuery>,
        IFilterSelectHandler<AppUser, AppUserProfileInfo, GetUserProfileInfoQuery>
    {
        public async Task<IEnumerable<SelectedAppUser>> Select(IQueryable<AppUser> query, GetUsersByFilterQuery filterModel)
        {
            return query.Select(u => new SelectedAppUser
            {
                Id = u.Id,
                UserName = u.UserName,
                AvatarImage = u.AvatarImage,
                IsSubscriber = filterModel.CurrentUserId.HasValue
                    ? u.Subscriptions.Any(s => s.SubscribedUserId == filterModel.CurrentUserId.Value)
                    : false,
                IsSubscription= filterModel.CurrentUserId.HasValue
                    ? u.Subscribers.Any(s => s.UserIdWhoSubscribed == filterModel.CurrentUserId.Value)
                    : false,
                ActiveBookCount = u.ActiveBooks.Count(),
                BookOpinionCount = u.BookOpinions.Count()
            }).ToList();
        }

        public async Task<AppUserProfileInfo> Select(IQueryable<AppUser> query, GetUserProfileInfoQuery filterModel)
        {
            return query.Where(u => u.Id == filterModel.UserId)
                .Select(u => new AppUserProfileInfo
                {
                    UserId = u.Id,
                    UserName = u.UserName,
                    AvatarImage = u.AvatarImage,
                    SubscribersCount = u.Subscribers.Count(),
                    SubscriptionsCount= u.Subscriptions.Count(),
                    ActiveBooks = u.ActiveBooks.Select(a => new ActiveBookInfoForProfile
                    {
                        Id = a.Id,
                        BookId= a.BookId,
                        BookTitle = a.Book.Title,
                        ImageData= a.Book.ImageData,
                        NumberPagesRead = a.NumberPagesRead,
                        TotalNumberPages = a.TotalNumberPages
                    })
                }).First();
        }
    }
}
