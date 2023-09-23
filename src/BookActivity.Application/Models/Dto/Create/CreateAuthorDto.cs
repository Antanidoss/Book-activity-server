namespace BookActivity.Application.Models.Dto.Create
{
    public class CreateAuthorDto : BaseCreateDto
    {
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }

        public override void Validate()
        {
            
        }
    }
}
