

using System.Collections.Generic;
using EatHealthyApi.Models;

namespace EatHealthyApi.Data.RefrentialData
{
    public interface IVarietyTypeDataRepo
    {
        bool saveChanges();
        IEnumerable<VarietyType> GetAllVarietyTypes();
        VarietyType GetVarietyTypeById(int id);
        void CreateVarietyType(VarietyType cmd);
        //void UpdateCommand(Command cmd);
        void DeleteVarietyType(VarietyType cmd);
    }
}