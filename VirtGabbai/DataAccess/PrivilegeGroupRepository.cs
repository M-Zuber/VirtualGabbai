using System;
using System.Collections.Generic;
using System.Linq;
using Framework;
using DataCache;
using System.Data;
using DataCache.Models;
using DataAccess.Interfaces;
using System.Data.Entity;

namespace DataAccess
{
    public class PrivilegeGroupRepository : IRepository<PrivilegesGroup>
    {
        private ZeraLeviContext _context;

        public DbSet<PrivilegesGroup> Entities => _context.PrivilegesGroups;

        public PrivilegeGroupRepository(ZeraLeviContext context)
        {
            _context = context;
        }

        public IEnumerable<PrivilegesGroup> Get() => Entities;

        public PrivilegesGroup GetByID(int id) => Entities.FirstOrDefault(pg => pg.ID == id);

        public PrivilegesGroup GetByGroupName(string name) => Entities.FirstOrDefault(pg => pg.GroupName.Equals(name, StringComparison.InvariantCultureIgnoreCase));

        public bool GroupNameExists(string name) => GetByGroupName(name) != null;

        public bool Exists(int id) => Entities.Any(pg => pg.ID == id);

        public bool Exists(PrivilegesGroup item) => item != null && Exists(item.ID);

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
                var current = GetByID(item.ID);

                if (current == null)
                {
                    current = new PrivilegesGroup();
                    Entities.Add(current);
                }

                current.GroupName = item.GroupName;
                current.Privileges = item.Privileges;
                _context.SaveChanges();
            }
        }
    }
}
