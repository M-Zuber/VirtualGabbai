using System;
using System.Collections.Generic;
using System.Linq;
using DataCache;
using Framework;
using DataCache.Models;
using DataAccess.Interfaces;
using System.Data.Entity;

namespace DataAccess
{
    public class AccountRepository : IReadOnlyRepository<Account>
    {
        private ZeraLeviContext _context;

        public DbSet<Account> Entities => _context.Accounts;

        public AccountRepository(ZeraLeviContext context)
        {
            _context = context;
        }

        public bool Exists(Account item) => item != null && Exists(item.ID);

        public bool Exists(int id) => Entities.Any(a => a.ID == id);

        public IEnumerable<Account> Get() => Entities;

        public Account GetByID(int id) => Entities.FirstOrDefault(a => a.ID == id);
    }
}
