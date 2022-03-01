using BookActivity.Domain.Constants;
using BookActivity.Domain.Helpers;
using System.ComponentModel.DataAnnotations;

namespace BookActivity.Domain.Models
{
    public class BookNote : BaseEntity
    {
        /// <summary>
        /// Text note
        /// </summary>
        [Required(ErrorMessage = ValidationErrorMessage.RequiredMessage)]
        [StringLength(2000, MinimumLength = 1, ErrorMessage = ValidationErrorMessage.StrLengthMessage)]
        public string Note
        {
            get
            {
                return note;
            }
            set
            {
                ValidationModelHelper.ValidateProperty(this, value, nameof(Note));
                note = value;
            }
        }
        private string note { get; set; }

        /// <summary>
        /// Note color
        /// </summary>
        public NoteColor NoteColor { get; set; }

        /// <summary>
        /// Relation of book note with the active book
        /// </summary>
        public ActiveBook ActiveBook { get; private set; }
        public int ActiveBookId { get; private set; }
    }

    public enum NoteColor
    {
        White,
        Red,
        Blue,
        Grean
    }
}
