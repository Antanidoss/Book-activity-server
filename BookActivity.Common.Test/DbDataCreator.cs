using BookActivity.Domain.Interfaces;
using BookActivity.Domain.Models;

namespace BookActivity.Common.Test
{
    public class DbDataCreator
    {
        public static async Task<Author> CreateAuthorAsync(IDbContext dbContext, string firstName = null, string surname = null)
        {
            Author author = new(
                firstName ?? DbConstants.AuthorFirstName,
                surname ?? DbConstants.AuthorSurname);

            await dbContext.Authors.AddAsync(author);

            await dbContext.SaveChangesAsync();

            return author;
        }

        public static async Task<Category> CreateCategoryAsync(IDbContext dbContext, string title = null)
        {
            Category category = new(title ?? DbConstants.CategoryTitle);

            await dbContext.Categories.AddAsync(category);

            await dbContext.SaveChangesAsync();

            return category;
        }

        public static async Task<Book> CreateBookAsync(
            IDbContext dbContext,
            string title = null,
            string description = null,
            Guid[] authorIds = null,
            Guid[] categoryIds = null,
            byte[] imageData = null)
        {
            Book book = new()
            {
                Title = title ?? DbConstants.BookTitle,
                Description = description ?? DbConstants.BookDescription,
                ImageData = imageData ?? DbConstants.ImageData,
                BookAuthors = authorIds == null
                    ? new BookAuthor[] { new() { AuthorId = (await CreateAuthorAsync(dbContext)).Id } }
                    : authorIds.Select(a => new BookAuthor { AuthorId = a }).ToArray(),
                BookCategories = categoryIds == null
                    ? new BookCategory[] { new() { CategoryId = (await CreateCategoryAsync(dbContext)).Id } }
                    : categoryIds.Select(c => new BookCategory { CategoryId = c }).ToArray()
            };

            await dbContext.Books.AddAsync(book);

            await dbContext.SaveChangesAsync();

            return book;
        }
    }
}
