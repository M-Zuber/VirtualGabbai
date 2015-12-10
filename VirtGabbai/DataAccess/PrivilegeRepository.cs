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

        public bool NameExists(string name) => _context.Privileges.FirstOrDefault(p => p.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase)) != null;

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
            var current = _context.Privileges.FirstOrDefault(p => p.ID == item.ID);

            if (current != null)
            {
                _context.Privileges.Remove(current);
                _context.SaveChanges();
            }
        }


        public void Save(Privilege item)
        {
            var current = _context.Privileges.FirstOrDefault(p => p.ID == item.ID);

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
