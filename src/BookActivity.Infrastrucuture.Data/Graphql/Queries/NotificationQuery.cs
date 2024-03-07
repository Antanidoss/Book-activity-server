using BookActivity.Domain.Models;
using BookActivity.Infrastructure.Data.EF;
using BookActivity.Shared.Models;
using HotChocolate.Data;
using HotChocolate.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace BookActivity.Infrastructure.Data.Graphql.Queries
{
    [ExtendObjectType("Query")]
    public class NotificationQuery
    {
        [UseOffsetPaging(IncludeTotalCount = true)]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Notification> GetNotifications([FromServices] BookActivityContext context, [FromServices] IServiceProvider serviceProvider)
        {
            var curUser = serviceProvider.GetService<CurrentUser>();

            if (curUser == null)
                return Enumerable.Empty<Notification>().AsQueryable();

            return context.Notifications.Where(n => n.ToUserId == curUser.Id);
        }
    }
}
