using System.Collections.Generic;
using EatHealthyApi.Models;

namespace EatHealthyApi.Data.Users
{
    public class MockEHUserRepo : IEHUserRepo
    {
        public void CreateUser(ApplicationUser userDetails)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteUser(ApplicationUser userDetails)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<ApplicationUser> GetAllUser()
        {
            throw new System.NotImplementedException();
        }

        public ApplicationUser GetUserById(string id)
        {
            throw new System.NotImplementedException();
        }

        public bool saveChanges()
        {
            throw new System.NotImplementedException();
        }

        public void UpdateUser(ApplicationUser userDetails)
        {
            throw new System.NotImplementedException();
        }
    }
}