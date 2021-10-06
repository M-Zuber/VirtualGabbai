using DataAccess.Interfaces;
using DataCache.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataAccess
{
    public class AccountRepository : IReadOnlyRepository<Account>
    {
        private readonly ZeraLeviContext _context;

        public DbSet<Account> Entities => _context.Accounts;

        public AccountRepository(ZeraLeviContext context)
        {
            _context = context;
        }

        public bool Exists(Account item) => item != null && Exists(item.Id);

        public bool Exists(int id) => Entities.Any(a => a.Id == id);

        public IEnumerable<Account> Get() => Entities;

        public Account GetById(int id) => Entities.FirstOrDefault(a => a.Id == id);
    }
}
