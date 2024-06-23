namespace BookActivity.Api.Common.Constants
{
    public static class ApiConstants
    {
        //Services
        public const string ActiveBookService = "activeBook";
        public const string BookNoteService = "bookNote";
        public const string BookService = "book";
        public const string AppUserService = "user";
        public const string AuthorService = "author";
        public const string BookRatingService = "bookOpinion";
        public const string ActiveBookStatisticService = "activeBookStatistic";
        public const string UserNotificationService = "userNotification";
        public const string OcrService = "ocr";

        //ActiveBook methods
        public const string AddActiveBookMethod = "add";
        public const string RemoveActiveBookMethod = "remove";
        public const string UpdateNumberPagesReadMethod = "updateNumberPagesRead";
        public const string UpdateBookNoteMethod = "updateBookNote";

        //Book methods
        public const string AddBookMethodMethod = "add";
        public const string GetBooksMethod = "get";
        public const string RemoveBookMethod = "remove";
        public const string UpdateBookMethod = "update";

        //AppUser methods
        public const string AddUserMethod = "add";
        public const string AuthenticationMethod = "authentication";
        public const string SubscribeAppUserMethod = "subscribeUser";
        public const string GetCurrentUserMethod = "getCurrentUser";
        public const string UpdateUserMethod = "update";
        public const string UnsubscribeAppUserMethod = "unsubscribe";

        //BookNote methods
        public const string AddBookNoteMethod = "add";

        //Author methods
        public const string AddAuthorMethod = "add";
        public const string GetAuthorByNameMethod = "getByName";

        //BookOpinion methods
        public const string AddBookOpinionMethod = "add";
        public const string AddBookOpinionDislikeMethod = "addDislike";
        public const string AddBookOpinionLikeMethod = "addLike";
        public const string RemoveBookOpinionDislikeMethod = "removeDislike";
        public const string RemoveBookOpinionLikeMethod = "removeLike";

        //ActiveBookStatistic methods
        public const string GetActiveBooksStaticMethod = "getActiveBooksStatic";
        public const string GetActiveBooksStatisticByDayMethod = "getActiveBooksStatisticByDay";

        //UserNotification methods
        public const string GetUserNotificationsMethod = "get";
        public const string RemoveUserNotificationsMethod = "remove";

        //Ocr methods
        public const string GetTextOnImageMethod = "get";
    }
}