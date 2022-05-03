using System;
using System.Collections.Generic;
using System.Linq;
using EatHealthyApi.Models;

namespace EatHealthyApi.Data.RefrentialData
{
    public class ActivityTypeDataRepo : IActivityTypeDataRepo
    {
        private readonly EhDBContext _context;

        public ActivityTypeDataRepo(EhDBContext context)
        {
            _context = context;
        }
        public void CreateActivityType(ActivityType cmd)
        {
            if (cmd == null)
            {
                throw new ArgumentNullException(nameof(cmd));
            }
            _context.ActivityType.Add(cmd);
        }

        public void DeleteActivityType(ActivityType cmd)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<ActivityType> GetAllActivityTypes()
        {
            return _context.ActivityType.Where(x=>x.isDeleted==false).ToList();
        }

        public ActivityType GetActivityTypeById(int id)
        {
            return _context.ActivityType.FirstOrDefault(predicate => predicate.Id == id);
        }

        public bool saveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}