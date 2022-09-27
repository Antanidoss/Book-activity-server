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

        //ActiveBook methods
        public const string AddActiveBookMethod = "add";
        public const string RemoveActiveBookMethod = "remove";
        public const string UpdateNumberPagesReadMethod = "updateNumberPagesRead";
        public const string UpdateBookNoteMethod = "updateBookNote";
        public const string GetActiveBooksByIdsMethod = "getById";
        public const string GetActiveBooksByUserIdMethod = "getByUserId";
        public const string GetActiveBookHistoryDataMethod = "getHistoryData";
        public const string GetActiveBooksByPaginationMethod = "getHistoryData";
        public const string GetActiveBooksByCurrentUserMethod = "getActiveBooksByCurrentUser";

        //Book methods
        public const string AddBookMethodMethod = "add";
        public const string GetBooksByIdsMethod = "getByIds";
        public const string GetBooksMethod = "get";
        public const string GetBooksByTitleContainsMethod = "getByTitleContains";
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

        //BookNote methods
        public const string AddBookNoteMethod = "add";

        //Author methods
        public const string AddAuthorMethod = "add";
        public const string GetAuthorByNameMethod = "getByName";
        public const string GetAllAuthorsMethod = "getAll";
    }
}