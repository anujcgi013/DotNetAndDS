using System.Collections.Generic;
using EatHealthyApi.Models;

namespace EatHealthyApi.Data.Users
{
    public interface IEHUserRepo
    {
         bool saveChanges();
         IEnumerable<ApplicationUser> GetAllUser();
        ApplicationUser GetUserById(string id);
        void CreateUser(ApplicationUser userDetails);
        void UpdateUser(ApplicationUser userDetails);
        void DeleteUser(ApplicationUser userDetails);
    }
}