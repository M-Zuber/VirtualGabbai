using DataAccess.Interfaces;
using DataCache.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DataAccess
{
    public class PhoneTypeRepository : IFullAccessRepository<PhoneType>
    {
        private ZeraLeviContext _context;

        public DbSet<PhoneType> Entities => _context.PhoneTypes;

        public PhoneTypeRepository(ZeraLeviContext context)
        {
            _context = context;
        }

        public IEnumerable<PhoneType> Get() => Entities;

        public PhoneType GetByID(int id) => Entities.FirstOrDefault(pt => pt.ID == id);

        public PhoneType GetByName(string name) => Entities.FirstOrDefault(pt => pt.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));

        public bool NameExists(string name) => GetByName(name) != null;

        public bool Exists(int id) => Entities.Any(pt => pt.ID == id);

        public bool Exists(PhoneType item) => item != null && Exists(item.ID);

        public void Add(PhoneType item)
        {
            if (item != null)
            {
                Entities.Add(item);
                _context.SaveChanges();
            }
        }

        public void Delete(PhoneType item)
        {
            if (Exists(item))
            {
                Entities.Remove(item);
                _context.SaveChanges();
            }
        }

        public void Save(PhoneType item)
        {
            if (item != null)
            {
                var current = GetByID(item.ID);

                if (current == null)
                {
                    current = new PhoneType();
                    current.Name = item.Name;
                    Entities.Add(current);
                }

                _context.SaveChanges();

                item.ID = current.ID;
            }
        }
    }
}
