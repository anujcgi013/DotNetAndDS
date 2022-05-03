

using System.Collections.Generic;
using EatHealthyApi.Models;

namespace EatHealthyApi.Data.RefrentialData
{
    public interface IGoalsDataRepo
    {
        bool saveChanges();
        IEnumerable<Goals> GetAllGoalItems();
        Goals GetGoalItemById(int id);
        void CreateGoalItem(Goals cmd);
        //void UpdateCommand(Command cmd);
        void DeleteGoalItem(Goals cmd);
    }
}