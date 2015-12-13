using System;
using System.Collections.Generic;
using System.Linq;
using Framework;
using DataCache;
using System.Data;
using DataCache.Models;
using DataAccess.Interfaces;

namespace DataAccess
{
    public class PrivilegeGroupRepository : IRepository<PrivilegesGroup>
    {
        private ZeraLeviContext _context;

        public PrivilegeGroupRepository(ZeraLeviContext context)
        {
            _context = context;
        }

        public IEnumerable<PrivilegesGroup> Get() => _context.PrivilegesGroups;

        public PrivilegesGroup GetByID(int id) => _context.PrivilegesGroups.FirstOrDefault(pg => pg.ID == id);

        public PrivilegesGroup GetByGroupName(string name) => _context.PrivilegesGroups.FirstOrDefault(pg => pg.GroupName.Equals(name, StringComparison.InvariantCultureIgnoreCase));

        public bool GroupNameExists(string name) => GetByGroupName(name) != null;

        public bool Exists(int id) => _context.PrivilegesGroups.Any(pg => pg.ID == id);

        public bool Exists(PrivilegesGroup item) => item != null && Exists(item.ID);

        public void Add(PrivilegesGroup item)
        {
            if (item != null)
            {
                _context.PrivilegesGroups.Add(item);
                _context.SaveChanges();
            }
        }

        public void Delete(PrivilegesGroup item)
        {
            if (Exists(item))
            {
                _context.PrivilegesGroups.Remove(item);
                _context.SaveChanges();
            }
        }

        public void Save(PrivilegesGroup item)
        {
            var current = GetByID(item.ID);

            if (current == null)
            {
                current = new PrivilegesGroup();
                _context.PrivilegesGroups.Add(current);
            }

            current.GroupName = item.GroupName;
            current.Privileges = item.Privileges;
            _context.SaveChanges();
        }
    }
}
