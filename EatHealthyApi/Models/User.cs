using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace EatHealthyApi.Models
{
    public class ApplicationUser: IdentityUser
    {
        [Column(TypeName ="nvarchar(150)")]
        public string FullName { get; set; }

        [Column(TypeName ="nvarchar(100)")]
        public string DOB { get; set; }

        [Column(TypeName ="nvarchar(50)")]
        public string Gender { get; set; }

        [Column(TypeName ="nvarchar(100)")]
        public string CreatedDate { get; set; }
        public bool isDeleted{get;set;}
    }
}