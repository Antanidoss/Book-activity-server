using BookActivity.Domain.Interfaces;
using BookActivity.Domain.Models;
using BookActivity.Shared.Models;

namespace BookActivity.Common.Test
{
    public class DbDataCreator
    {
        public static async Task<Author> CreateAuthorAsync(IDbContext dbContext, string firstName, string surname)
        {
            Author author = new(firstName, surname);

            await dbContext.Authors.AddAsync(author);

            await dbContext.SaveChangesAsync();

            return author;
        }

        public static async Task<Category> CreateCategoryAsync(IDbContext dbContext, string title)
        {
            Category category = new(title);

            await dbContext.Categories.AddAsync(category);

            await dbContext.SaveChangesAsync();

            return category;
        }
    }
}
