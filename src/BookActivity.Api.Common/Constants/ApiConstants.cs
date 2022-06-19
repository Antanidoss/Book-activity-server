namespace BookActivity.Api.Common.Constants
{
    public sealed class ApiConstants
    {
        //Services
        public const string ActiveBookService = "activeBook";
        public const string BookNoteService = "bookNote";
        public const string BookService = "book";
        public const string AppUserService = "user";

        //ActiveBook methods
        public const string AddActiveBookMethod = "addActiveBook";
        public const string RemoveActiveBookMethod = "removeActiveBook";
        public const string UpdateNumberPagesReadMethod = "updateNumberPagesRead";
        public const string UpdateBookNoteMethod = "updateBookNote";
        public const string GetActiveBooksByIdsMethod = "getActiveBookByIds";
        public const string GetActiveBooksByUserIdMethod = "getActiveBooksByUserId";

        //Book methods
        public const string AddBookMethodMethod = "addBook";
        public const string GetBooksByIdsMethod = "getBooksByIds";
        public const string GetBooksByTitleContainsMethod = "getBooksByTitleContains";

        //AppUser methods
        public const string AddUserMethod = "addUser";
        public const string GetUserByIdMethod = "getUserById";
        public const string AuthenticationMethod = "authentication";
        public const string SubscribeAppUserMethod = "subscribeUser";

        //BookNote methods
        public const string AddBookNoteMethod = "addBookNote";
    }
}