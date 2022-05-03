
using System.ComponentModel.DataAnnotations;

namespace EatHealthyApi.Models
{
    public class Goals
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }
        
        [MaxLength(256)]
        public string Descritpion { get; set; }

        [MaxLength(100)]
        public string CreatedDate { get; set; }

        public bool isDeleted { get; set; }
    }
}