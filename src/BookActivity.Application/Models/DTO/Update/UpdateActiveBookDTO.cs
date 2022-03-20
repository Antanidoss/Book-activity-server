using System;

namespace BookActivity.Application.Models.DTO.Update
{
    public class UpdateActiveBookDTO : BaseUpdateDTO
    {
        public int NumberPagesRead { get; set; }

        public UpdateActiveBookDTO(Guid activeBookId, int numberPagesRead) : base(activeBookId)
        {
            NumberPagesRead = numberPagesRead;
        }
    }
}