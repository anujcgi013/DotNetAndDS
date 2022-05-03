
using System.ComponentModel.DataAnnotations;

namespace EatHealthyApi.Dtos.ReferentialDataDtos
{
    public class GoalCreateDto
    {
        [MaxLength(256)]
       public string Name { get; set; }
    }
}