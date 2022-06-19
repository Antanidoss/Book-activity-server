using System;

namespace BookActivity.Application.Models.DTO.Update
{
    public sealed class UpdateNumberPagesReadDTO
    {
        public Guid ActiveBookId { get; set; }
        public int NumberPagesRead { get; set; }

        public UpdateNumberPagesReadDTO() { }
        public UpdateNumberPagesReadDTO(Guid activeBookId, int numberPagesRead)
        {
            ActiveBookId = activeBookId;
            NumberPagesRead = numberPagesRead;
        }
    }
}