using BookActivity.Shared.Models;

namespace BookActivity.Common.Test
{
    public static class DbConstants
    {
        public static CurrentUser CurrentUser = new()
        {
            Id = Guid.NewGuid(),
            UserName = "Anton"
        };

        public const string CurrentUserEmail = "antonBookActivity@gmail.com";

        public const string AuthorFirstName = "Jack";
        public const string AuthorSurname = "London";

        public const string BookTitle = "Martin Eden";
        public const string BookDescription = "Martin Eden is a modern Cinderella tale. Placing the oligarchy on the target board, Jack London asks the question: " +
            "“Can a Cinderella become a Princess? Or should she have a noble family to be a Princess?”";

        public const string CategoryTitle = "Artistic literature";
    }
}
