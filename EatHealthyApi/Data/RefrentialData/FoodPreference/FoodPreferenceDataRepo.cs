using System;
using System.Collections.Generic;
using System.Linq;
using EatHealthyApi.Models;

namespace EatHealthyApi.Data.RefrentialData
{
    public class FoodPreferenceDataRepo : IFoodPreferenceDataRepo
    {
        private readonly EhDBContext _context;

        public FoodPreferenceDataRepo(EhDBContext context)
        {
            _context = context;
        }
        public void CreateFoodPreferenceItem(FoodPreferences cmd)
        {
            if (cmd == null)
            {
                throw new ArgumentNullException(nameof(cmd));
            }
            _context.FoodPreference.Add(cmd);
        }

        public void DeleteFoodPreferenceItem(FoodPreferences cmd)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<FoodPreferences> GetAllFoodPreferenceItems()
        {
            return _context.FoodPreference.Where(x=>x.isDeleted==false).ToList();
        }

        // public FoodPreferences GetFoodPreferenceItemById(int id)
        // {
        //     return _context.FoodPreference(predicate => predicate.Id == id);
        // }

        public bool saveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}