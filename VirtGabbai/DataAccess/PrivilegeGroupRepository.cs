using DataAccess.Interfaces;
using DataCache.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataAccess
{
    public class PrivilegeGroupRepository : IFullAccessRepository<PrivilegesGroup>
    {
        private readonly ZeraLeviContext _context;

        public DbSet<PrivilegesGroup> Entities => _context.PrivilegesGroups;

        public PrivilegeGroupRepository(ZeraLeviContext context)
        {
            _context = context;
        }

        public IEnumerable<PrivilegesGroup> Get() => Entities;

        public PrivilegesGroup GetById(int id) => Entities.FirstOrDefault(pg => pg.Id == id);

        public PrivilegesGroup GetByGroupName(string name) => Entities.FirstOrDefault(pg => pg.GroupName.Equals(name, StringComparison.InvariantCultureIgnoreCase));

        public bool GroupNameExists(string name) => GetByGroupName(name) != null;

        public bool Exists(int id) => Entities.Any(pg => pg.Id == id);

        public bool Exists(PrivilegesGroup item) => item != null && Exists(item.Id);

        public void Add(PrivilegesGroup item)
        {
            if (item != null)
            {
                Entities.Add(item);
                _context.SaveChanges();
            }
        }

        public void Delete(PrivilegesGroup item)
        {
            if (Exists(item))
            {
                Entities.Remove(item);
                _context.SaveChanges();
            }
        }

        public void Save(PrivilegesGroup item)
        {
            if (item != null)
            {
                var current = GetById(item.Id);

                if (current == null)
                {
                    current = new PrivilegesGroup { GroupName = item.GroupName, Privileges = item.Privileges };
                    Entities.Add(current);
                }

                _context.SaveChanges();

                item.Id = current.Id;
            }
        }
    }
}
