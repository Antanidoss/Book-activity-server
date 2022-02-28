using System;

namespace BookActivity.Domain.Models
{
    public class BaseEntity
    {
        public int Id { get; private set; }
        public DateTime TimeOfCreation { get; set; }
        public DateTime TimeOfUpdate { get; set; }

        protected BaseEntity() { }
    }
}