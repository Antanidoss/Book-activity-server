using System;

namespace BookActivity.Domain.Models
{
    public class BookNote : BaseEntity
    {
        /// <summary>
        /// Text note
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// Note color
        /// </summary>
        public NoteColor NoteColor { get; set; }

        /// <summary>
        /// Relation of book note with the active book
        /// </summary>
        public ActiveBook ActiveBook { get; private set; }
        public Guid ActiveBookId { get; private set; }
    }

    public enum NoteColor
    {
        White,
        Red,
        Blue,
        Grean
    }
}
