using NetDevPack.Messaging;
using System;

namespace BookActivity.Domain.Commands.BookRatingCommands
{
    public class BookRatingCommand : Command
    {
        public Guid Id { get; set; }
    }
}
