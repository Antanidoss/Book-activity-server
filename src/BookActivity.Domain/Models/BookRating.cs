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

        public float CalculateAverageRating()
        {
            if (BookOpinions == null)
                return -1;

            if (!BookOpinions.Any())
                return 0;

            return BookOpinions.Count() / BookOpinions.Sum(o => o.Grade) * 100;
        }
    }
}
