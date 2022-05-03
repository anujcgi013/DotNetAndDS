using System;
using System.Collections.Generic;
using System.Linq;
using EatHealthyApi.Models;

namespace EatHealthyApi.Data.RefrentialData
{
    public class DislikeDataRepo : IDislikeDataRepo
    {
        private readonly EhDBContext _context;

        public DislikeDataRepo(EhDBContext context)
        {
            _context = context;
        }
        public void CreateDislikeItem(DislikeItems cmd)
        {
            if (cmd == null)
            {
                throw new ArgumentNullException(nameof(cmd));
            }
            _context.DislikeItems.Add(cmd);
        }

        public void DeleteDislikeItem(DislikeItems cmd)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<DislikeItems> GetAllDislikeItems()
        {
            return _context.DislikeItems.Where(x=>x.isDeleted==false).ToList();
        }

        public DislikeItems GetDislikeItemById(int id)
        {
            return _context.DislikeItems.FirstOrDefault(predicate => predicate.Id == id);
        }

        public bool saveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}