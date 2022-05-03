using System;
using System.Collections.Generic;
using System.Linq;
using EatHealthyApi.Models;

namespace EatHealthyApi.Data.RefrentialData
{
    public class GoalsDataRepo : IGoalsDataRepo
    {
        private readonly EhDBContext _context;

        public GoalsDataRepo(EhDBContext context)
        {
            _context = context;
        }
        public void CreateGoalItem(Goals cmd)
        {
            if (cmd == null)
            {
                throw new ArgumentNullException(nameof(cmd));
            }
            _context.Goals.Add(cmd);
        }

        public void DeleteGoalItem(Goals cmd)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Goals> GetAllGoalItems()
        {
            return _context.Goals.Where(x=>x.isDeleted==false).ToList();
        }

        public Goals GetGoalItemById(int id)
        {
            return _context.Goals.FirstOrDefault(predicate => predicate.Id == id);
        }

        public bool saveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}