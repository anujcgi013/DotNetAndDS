
using System;
using System.Collections.Generic;
using System.Linq;
using EatHealthyApi.Models;

namespace EatHealthyApi.Data.Users
{
    public class SqlEHUserRepo : IEHUserRepo
    {
        private readonly EhDBContext _context;
         public SqlEHUserRepo(EhDBContext context)
        {
            _context = context;
        }
        public void CreateUser(ApplicationUser userDetails)
        {
           if(userDetails==null)
           {
               throw new ArgumentNullException(nameof(userDetails));
           }
           _context.Users.Add(userDetails);
        }

        public void DeleteUser(ApplicationUser userDetails)
        {
            if(userDetails==null)
           {
               throw new ArgumentNullException(nameof(userDetails));
           }
           _context.Users.Remove(userDetails);
        }

        public IEnumerable<ApplicationUser> GetAllUser()
        {
            return _context.EHUser.ToList();
        }

        public ApplicationUser GetUserById(string id)
        {
            return _context.EHUser.FirstOrDefault(p => p.Id == id);
        }

        public bool saveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateUser(ApplicationUser userDetails)
        {
            throw new System.NotImplementedException();
        }
    }
}