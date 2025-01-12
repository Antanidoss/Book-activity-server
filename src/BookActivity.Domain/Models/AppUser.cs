﻿using BookActivity.Domain.Core;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace BookActivity.Domain.Models
{
    public sealed class AppUser : IdentityUser<Guid>, IAggregateRoot
    {
        public ICollection<Subscriber> Subscribers { get; set; }
        public ICollection<Subscription> Subscriptions { get; set; }
        public IEnumerable<BookOpinion> BookOpinions { get; set; }
        public ICollection<Notification> Notifications { get; set; }
        public ICollection<ActiveBook> ActiveBooks { get; set; }
        public ICollection<BookNoteLike> BookNoteLikes { get; set; }
        public ICollection<BookNoteDislike> BookNoteDislikes { get; set; }
        public ICollection<BookOpinionLike> BookOpinionLikes { get; set; }
        public ICollection<BookOpinionDislike> BookOpinionDislikes { get; set; }
        public byte[] AvatarImage { get; set; }
        public string Description { get; set; }

        public AppUser(string userName, string email) : base()
        {
            UserName = userName;
            Email = email;
        }

        public AppUser(string userName, byte[] avatarImage, string email) : base()
        {
            UserName = userName;
            AvatarImage = avatarImage;
            Email = email;
        }

        //private, parameterless constructor used by EF Core
        private AppUser() { }
    }
}