using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookActivity.Domain.Commands.ActiveBookCommands.Validations
{
    public class AddNewActiveBookCommandValidation : ActiveBookValidation<AddBookActiveCommand>
    {
        public AddNewActiveBookCommandValidation()
        {
            ValidateNumberPagesRead();
            ValidateTotalNumberPages();
            ValidateActiveBookId();
            ValidateUserId();
        }
    }
}
