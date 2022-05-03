
using System.ComponentModel.DataAnnotations;

namespace EatHealthyApi.Dtos.ReferentialDataDtos
{
    public class DislikeCreateDto
    {
        [MaxLength(256)]
       public string Name { get; set; }
    }
}