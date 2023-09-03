namespace BookActivity.Api.Common.Constants
{
    public sealed class ApiConstants
    {
        //Services
        public const string ActiveBookService = "activeBook";
        public const string BookNoteService = "bookNote";
        public const string BookService = "book";
        public const string AppUserService = "user";
        public const string AuthorService = "author";
        public const string BookRatingService = "bookRating";
        public const string ActiveBookStatisticService = "activeBookStatistic";
        public const string UserNotificationService = "userNotification";

        //ActiveBook methods
        public const string AddActiveBookMethod = "add";
        public const string RemoveActiveBookMethod = "remove";
        public const string UpdateNumberPagesReadMethod = "updateNumberPagesRead";
        public const string UpdateBookNoteMethod = "updateBookNote";
        public const string GetActiveBookHistoryDataMethod = "getHistoryData";

        //Book methods
        public const string AddBookMethodMethod = "add";
        public const string GetBooksMethod = "get";
        public const string RemoveBookMethod = "remove";
        public const string UpdateBookMethod = "update";
        public const string GetBookHistoryDataMethod = "getHistoryData";

        //AppUser methods
        public const string AddUserMethod = "add";
        public const string GetUserByIdMethod = "getById";
        public const string AuthenticationMethod = "authentication";
        public const string SubscribeAppUserMethod = "subscribeUser";
        public const string GetCurrentUserMethod = "getCurrentUser";
        public const string UpdateUserMethod = "update";
        public const string GetUserByFilterMethod = "getUsersByFilter";
        public const string UnsubscribeAppUserMethod = "unsubscribe";
        public const string GetUserProfileInfoMethod = "getUserProfileInfo";

        //BookNote methods
        public const string AddBookNoteMethod = "add";

        //Author methods
        public const string AddAuthorMethod = "add";
        public const string GetAuthorByNameMethod = "getByName";

        //BookRating methods
        public const string UpdateBookRatingMethod = "update";

        //ActiveBookStatistic methods
        public const string GetActiveBooksStaticMethod = "getActiveBooksStatic";

        //UserNotification methods
        public const string GetUserNotificationsMethod = "get";
        public const string RemoveUserNotificationsMethod = "remove";
    }
}