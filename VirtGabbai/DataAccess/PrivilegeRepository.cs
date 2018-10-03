using DataAccess.Interfaces;
using DataCache.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataAccess
{
    public class PrivilegeRepository : IReadOnlyRepository<Privilege>
    {
        private readonly ZeraLeviContext _context;

        public DbSet<Privilege> Entities => _context.Privileges;

        public PrivilegeRepository(ZeraLeviContext context)
        {
            _context = context;
        }

        public IEnumerable<Privilege> Get() => Entities;

        public Privilege GetById(int id) => Entities.FirstOrDefault(p => p.Id == id);

        public Privilege GetByName(string name) => Entities.FirstOrDefault(p => p.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));

        public bool NameExists(string name) => GetByName(name) != null;

        public bool Exists(int id) => Entities.Any(p => p.Id == id);

        public bool Exists(Privilege item) => item != null && Exists(item.Id);
    }
}
