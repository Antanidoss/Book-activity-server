using System;

namespace BookActivity.Domain.Models
{
    public class BaseEntity
    {
        public int Id { get; private set; }

        /// <summary>
        /// Time of creation entity
        /// </summary>
        public DateTime TimeOfCreation { get; set; }

        /// <summary>
        /// Time of update entity
        /// </summary>
        public DateTime TimeOfUpdate { get; set; }

        /// <summary>
        /// This entity can be shown to the user
        /// </summary>
        public bool IsPublic { get; set; }

        protected BaseEntity() { }
        public BaseEntity(bool isPublic)
        {
            IsPublic = isPublic;
        }
    }
}