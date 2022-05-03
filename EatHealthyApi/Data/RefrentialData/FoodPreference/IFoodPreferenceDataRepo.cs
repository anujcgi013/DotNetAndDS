

using System.Collections.Generic;
using EatHealthyApi.Models;

namespace EatHealthyApi.Data.RefrentialData
{
    public interface IFoodPreferenceDataRepo
    {
        bool saveChanges();
        IEnumerable<FoodPreferences> GetAllFoodPreferenceItems();
        // FoodPreferences GetFoodPreferenceItemById(int id);
        void CreateFoodPreferenceItem(FoodPreferences cmd);
        //void UpdateCommand(Command cmd);
        void DeleteFoodPreferenceItem(FoodPreferences cmd);
    }
}