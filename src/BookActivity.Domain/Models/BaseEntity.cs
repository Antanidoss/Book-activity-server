using NetDevPack.Domain;
using System;

namespace BookActivity.Domain.Models
{
    public class BaseEntity : Entity, IAggregateRoot
    {
        /// <summary>
        /// Time of creation entity
        /// </summary>
        public DateTime TimeOfCreation { get; set; }

        /// <summary>
        /// Time of update entity
        /// </summary>
        public DateTime TimeOfUpdate { get; set; }

        protected BaseEntity() { }
    }
}