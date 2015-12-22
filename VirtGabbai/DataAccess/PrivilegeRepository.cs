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
    public class PrivilegeRepository : IReadOnlyRepository<Privilege>
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
    }
}
