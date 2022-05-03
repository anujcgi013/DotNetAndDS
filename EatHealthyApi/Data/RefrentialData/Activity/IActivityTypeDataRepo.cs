

using System.Collections.Generic;
using EatHealthyApi.Models;

namespace EatHealthyApi.Data.RefrentialData
{
    public interface IActivityTypeDataRepo
    {
        bool saveChanges();
        IEnumerable<ActivityType> GetAllActivityTypes();
        ActivityType GetActivityTypeById(int id);
        void CreateActivityType(ActivityType cmd);
        //void UpdateCommand(Command cmd);
        void DeleteActivityType(ActivityType cmd);
    }
}