using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace  EatHealthyApi.Models
{
    public class UserDislikes
    {
         [Key]
        public int Id { get; set; }

        public Guid UserId { get; set; }
 
         public int DislikeItemId { get; set; }

         [MaxLength(256)]
        public string CreatedDate { get; set; }

        [MaxLength(256)]
        public bool isDeleted { get; set; }
    }
}
