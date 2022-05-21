namespace BookActivity.Api.Common.Constants
{
    public sealed class ApiConstants
    {
        //Services
        public const string ActiveBookService = "activeBook";
        public const string BookService = "book";
        public const string AppUserService = "user";

        //ActiveBook methods
        public const string AddActiveBookMethod = "addActiveBook";
        public const string RemoveActiveBookMethod = "removeActiveBook";
        public const string UpdateActiveBookMethod = "updateActiveBook";
        public const string GetActiveBooksMethod = "getActiveBook";

        //Book methods
        public const string AddBookMethod = "addBook";
        public const string GetBooksMethod = "getBooks";

        //AppUser methods
        public const string AddUser = "addUser";
        public const string GetUserById = "getUserById";
    }
}