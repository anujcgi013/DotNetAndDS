
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EatHealthyApi.Models;

namespace EatHealthyApi.Dtos.ReferentialDataDtos
{
    public class ReferentialCreateDtos
    {
       public IEnumerable<DislikeReadDto> Dislikes{get;set;}
       public IEnumerable<ActivityTypeReadDto> Activities{get;set;}
       public IEnumerable<FoodPreferencesReadDto> FoodPreferences{get;set;}
       public IEnumerable<GoalReadDto> Goals{get;set;}
       public IEnumerable<VarietyTypeReadDto> Varities{get;set;}
    }
}