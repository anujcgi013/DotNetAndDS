

using System;
using System.ComponentModel.DataAnnotations;

namespace  EatHealthyApi.Models
{
    public class PersonalDetails
    {
        public int Id {get;set;}
        public Guid UserId{get;set;}
        public int Age{get;set;}
        public int Height{get;set;}

        public int CurrentWeight{get;set;}
        public int TargetWeight{get;set;}

        public int GoalId{get;set;}
        
        public int ActivityLevelId{get;set;}
        public int VarietyId{get;set;}

        public int CalculatedCalories{get;set;}

        [MaxLength(256)]
        public string CreatedDate { get; set; }

        [MaxLength(256)]
        public bool isDeleted { get; set; }
    }
    
}