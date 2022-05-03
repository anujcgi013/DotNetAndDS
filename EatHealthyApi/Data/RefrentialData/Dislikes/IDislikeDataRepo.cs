

using System.Collections.Generic;
using EatHealthyApi.Models;

namespace EatHealthyApi.Data.RefrentialData
{
    public interface IDislikeDataRepo
    {
        bool saveChanges();
        IEnumerable<DislikeItems> GetAllDislikeItems();
        DislikeItems GetDislikeItemById(int id);
        void CreateDislikeItem(DislikeItems cmd);
        //void UpdateCommand(Command cmd);
        void DeleteDislikeItem(DislikeItems cmd);
    }
}