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
            Book book = new(
                title ?? DbConstants.BookTitle,
                description ?? DbConstants.BookDescription,
                imageData ?? DbConstants.ImageData,
                authorIds == null
                    ? new BookAuthor[] { new(await CreateAuthorAsync(dbContext)) }
                    : authorIds.Select(a => new BookAuthor(a)).ToArray(),
                categoryIds == null
                    ? new BookCategory[] { new(await CreateCategoryAsync(dbContext)) }
                    : categoryIds.Select(c => new BookCategory(c)).ToArray()
                );

            await dbContext.Books.AddAsync(book);
            await dbContext.SaveChangesAsync();

            return book;
        }
    }
}
