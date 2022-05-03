
using System.ComponentModel.DataAnnotations;

namespace EatHealthyApi.Dtos.UserDtos
{
    public class UserReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        //public int PhoneNumber { get; set; }
        public string DOB { get; set; }
        public string Gender { get; set; }

        public string CreatedDate { get; set; }
        public bool isDeleted{get;set;}
    }
}