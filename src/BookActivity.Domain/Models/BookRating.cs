using BookActivity.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookActivity.Domain.Models
{
    public class BookRating : BaseEntity
    {
        public Book Book { get; set; }
        public Guid BookId { get; set; }

        public IList<BookOpinion> BookOpinions { get; set; }

        public float GetAverageRating()
        {
            if (BookOpinions == null || !BookOpinions.Any())
                return 0;

            return BookOpinions.Average(b => b.Grade);
        }
    }
}
