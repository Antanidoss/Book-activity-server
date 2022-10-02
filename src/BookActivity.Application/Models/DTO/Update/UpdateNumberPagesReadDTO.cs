using System;

namespace BookActivity.Application.Models.Dto.Update
{
    public sealed class UpdateNumberPagesReadDto
    {
        public Guid ActiveBookId { get; set; }
        public int NumberPagesRead { get; set; }

        public UpdateNumberPagesReadDto() { }
        public UpdateNumberPagesReadDto(Guid activeBookId, int numberPagesRead)
        {
            ActiveBookId = activeBookId;
            NumberPagesRead = numberPagesRead;
        }
    }
}