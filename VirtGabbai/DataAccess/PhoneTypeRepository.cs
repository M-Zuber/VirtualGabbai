using DataAccess.Interfaces;
using DataCache.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class PhoneTypeRepository : IRepository<PhoneType>
    {
        private ZeraLeviContext _context;

        public PhoneTypeRepository(ZeraLeviContext context)
        {
            _context = context;
        }

        public IEnumerable<PhoneType> Get() => _context.PhoneTypes;

        public PhoneType GetByID(int id) => _context.PhoneTypes.FirstOrDefault(pt => pt.ID == id);

        public PhoneType GetByName(string name) => _context.PhoneTypes.FirstOrDefault(pt => pt.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));

        public bool NameExists(string name) => GetByName(name) != null;

        public bool Exists(int id) => _context.PhoneTypes.Any(pt => pt.ID == id);

        public bool Exists(PhoneType item) => item != null && Exists(item.ID);

        public void Add(PhoneType item)
        {
            if (item != null)
            {
                _context.PhoneTypes.Add(item);
                _context.SaveChanges();
            }
        }

        public void Delete(PhoneType item)
        {
            if (Exists(item))
            {
                _context.PhoneTypes.Remove(item);
                _context.SaveChanges();
            }
        }

        public void Save(PhoneType item)
        {
            var current = GetByID(item.ID);

            if (current == null)
            {
                current = new PhoneType();
                _context.PhoneTypes.Add(current);
            }

            current.Name = item.Name;
            _context.SaveChanges();
        }
    }
}
