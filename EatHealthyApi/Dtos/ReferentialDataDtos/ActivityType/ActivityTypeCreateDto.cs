
using System.ComponentModel.DataAnnotations;

namespace EatHealthyApi.Dtos.ReferentialDataDtos
{
    public class ActivityTypeCreateDto
    {
        [MaxLength(256)]
       public string Name { get; set; }
    }
}