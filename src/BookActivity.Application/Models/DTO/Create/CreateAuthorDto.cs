﻿namespace BookActivity.Application.Models.DTO.Create
{
    public class CreateAuthorDto : BaseCreateDto
    {
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
    }
}