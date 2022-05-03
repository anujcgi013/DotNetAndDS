using System.ComponentModel.DataAnnotations;

namespace EatHealthyApi.Dtos.UserDtos
{
    public class UseLoginDto
    {
        [Required]
        [MaxLength(256)]
        public string Email { get; set; }
        
        [Required]
        public string password { get; set; }
    }
}