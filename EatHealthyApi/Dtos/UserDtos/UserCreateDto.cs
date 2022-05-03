using System.ComponentModel.DataAnnotations;

namespace EatHealthyApi.Dtos.UserDtos
{
    public class UserCreateDto
    {
        [Required]
        [MaxLength(150)]
        public string FullName { get; set; }
        [Required]
        [MaxLength(256)]
        public string Email { get; set; }
        // [MaxLength(100)]
        // public int PhoneNumber { get; set; }
        [Required]
        public string password {get;set;}
        [Required]
        public string DOB { get; set; }
        [Required]
        public string Gender { get; set; }
    }
}