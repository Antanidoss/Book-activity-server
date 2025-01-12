﻿using BookActivity.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookActivity.Domain.Models
{
    public sealed class Book : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IEnumerable<BookAuthor> BookAuthors { get; init; }
        public byte[] ImageData { get; set; }
        public ICollection<ActiveBook> ActiveBooks { get; init; }
        public ICollection<BookOpinion> BookOpinions { get; set; }
        public ICollection<BookCategory> BookCategories { get; set; }

        public Book(string title, string description, byte[] imageData, IEnumerable<BookAuthor> bookAuthors, params BookCategory[] bookCategories)
        {
            Title = title;
            Description = description;
            BookAuthors = bookAuthors.ToList();
            BookCategories = bookCategories.ToList();
            ImageData = imageData;
        }

        public Book(Guid bookId) : base(bookId) { }

        //private, parameterless constructor used by EF Core
        private Book() { }

        public float GetAverageRating()
        {
            if (BookOpinions == null || !BookOpinions.Any())
                return 0;

            return BookOpinions.Average(b => b.Grade);
        }
    }
}