namespace BookActivity.Application.Models.Dto.Create
{
    public class CreateAuthorDto : BaseDto
    {
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }

        public override string Validate()
        {
            return string.Empty;
        }
    }
}
