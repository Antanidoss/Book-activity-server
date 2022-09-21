using NetDevPack.Messaging;
using System;

namespace BookActivity.Domain.Commands.AuthorCommands
{
    public class AuthorCommand : Command
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
    }
}
