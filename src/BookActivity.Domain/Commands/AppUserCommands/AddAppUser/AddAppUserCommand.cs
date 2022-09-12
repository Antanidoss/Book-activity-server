namespace BookActivity.Domain.Commands.AppUserCommands.AddAppUser
{
    public sealed class AddAppUserCommand : AppUserCommand
    {
        public AddAppUserCommand() { }
        public AddAppUserCommand(string name, string email, string password)
        {
            Name = name;
            Email = email;
            Password = password;
        }
    }
}
