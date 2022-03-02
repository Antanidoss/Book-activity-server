using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookActivity.Domain.Commands.BookActiveCommands.Validations
{
    public class AddNewActiveBookCommandValidation : ActiveBookValidation<AddNewBookActiveCommand>
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
