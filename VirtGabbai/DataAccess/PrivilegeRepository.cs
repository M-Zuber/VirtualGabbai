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
    public class PrivilegeRepository : IRepository<Privilege>
    {
        private ZeraLeviContext _context;

        public DbSet<Privilege> Entities => _context.Privileges;

        public PrivilegeRepository(ZeraLeviContext context)
        {
            _context = context;
        }

        public IEnumerable<Privilege> Get() => Entities;

        public Privilege GetByID(int id) => Entities.FirstOrDefault(p => p.ID == id);

        public Privilege GetByName(string name) => Entities.FirstOrDefault(p => p.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));

        public bool NameExists(string name) => GetByName(name) != null;

        public bool Exists(int id) => Entities.Any(p => p.ID == id);

        public bool Exists(Privilege item) => item != null && Exists(item.ID);

        public void Add(Privilege item)
        {
            if (item != null)
            {
                Entities.Add(item);
                _context.SaveChanges();
            }
        }

        public void Delete(Privilege item)
        {
            if (Exists(item))
            {
                Entities.Remove(item);
                _context.SaveChanges();
            }
        }


        public void Save(Privilege item)
        {
            var current = GetByID(item.ID);

            if (current == null)
            {
                current = new Privilege();
                Entities.Add(current);
            }

            current.Name = item.Name;
            _context.SaveChanges();
        }
    }
}
