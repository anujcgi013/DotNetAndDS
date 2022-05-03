using System;
using System.Collections.Generic;
using System.Linq;
using EatHealthyApi.Models;

namespace EatHealthyApi.Data.RefrentialData
{
    public class VarietyTypeDataRepo : IVarietyTypeDataRepo
    {
        private readonly EhDBContext _context;

        public VarietyTypeDataRepo(EhDBContext context)
        {
            _context = context;
        }
        public void CreateVarietyType(VarietyType cmd)
        {
            if (cmd == null)
            {
                throw new ArgumentNullException(nameof(cmd));
            }
            _context.VarietyType.Add(cmd);
        }

        public void DeleteVarietyType(VarietyType cmd)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<VarietyType> GetAllVarietyTypes()
        {
            return _context.VarietyType.Where(x=>x.isDeleted==false).ToList();
        }

        public VarietyType GetVarietyTypeById(int id)
        {
            return _context.VarietyType.FirstOrDefault(predicate => predicate.Id == id);
        }

        public bool saveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}