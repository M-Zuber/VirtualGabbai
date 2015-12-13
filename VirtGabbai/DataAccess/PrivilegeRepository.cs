using DataAccess.Interfaces;
using DataCache.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class PrivilegeRepository : IRepository<Privilege>
    {
        private ZeraLeviContext _context;

        public PrivilegeRepository(ZeraLeviContext context)
        {
            _context = context;
        }

        public IEnumerable<Privilege> Get() => _context.Privileges;

        public Privilege GetByID(int id) => _context.Privileges.FirstOrDefault(p => p.ID == id);

        public Privilege GetByName(string name) => _context.Privileges.FirstOrDefault(p => p.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));

        public bool NameExists(string name) => GetByName(name) != null;

        public bool Exists(int id) => _context.Privileges.Any(p => p.ID == id);

        public bool Exists(Privilege item) => item != null && Exists(item.ID);

        public void Add(Privilege item)
        {
            if (item != null)
            {
                _context.Privileges.Add(item);
                _context.SaveChanges();
            }
        }

        public void Delete(Privilege item)
        {
            if (Exists(item))
            {
                _context.Privileges.Remove(item);
                _context.SaveChanges();
            }
        }


        public void Save(Privilege item)
        {
            var current = GetByID(item.ID);

            if (current == null)
            {
                current = new Privilege();
                _context.Privileges.Add(current);
            }

            current.Name = item.Name;
            _context.SaveChanges();
        }
    }
}
