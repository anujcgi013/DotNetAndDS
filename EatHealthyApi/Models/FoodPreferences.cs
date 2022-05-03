
using System.ComponentModel.DataAnnotations;

namespace EatHealthyApi.Models
{
    public class FoodPreferences
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(256)]
        public string Name { get; set; }

        [MaxLength(256)]
        public string CreatedDate { get; set; }

        [MaxLength(256)]
        public bool isDeleted { get; set; }
    }
}