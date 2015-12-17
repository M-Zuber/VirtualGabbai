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
    public class AccountRepository : IFullAccessRepository<Account>
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

        public void Add(Account item)
        {
            if (ValidateItem(item))
            {
                Entities.Add(item);
                _context.SaveChanges();
            }
        }

        public void Delete(Account item)
        {
            if (Exists(item))
            {
                Entities.Remove(item);
                _context.SaveChanges();
            }
        }

        public void Save(Account item)
        {
            if (ValidateItem(item))
            {
                var current = GetByID(item.ID);

                if (current == null)
                {
                    current = new Account() { Person = item.Person };
                    Entities.Add(current);

                    // Update the item passed in, in case the caller does something with it
                    // THINK: should it just return current?
                    item.ID = current.ID;
                    item.PersonID = current.PersonID;
                }

                current.Donations = item.Donations;
                current.LastMonthlyPaymentDate = item.LastMonthlyPaymentDate;
                current.MonthlyPaymentAmount = item.MonthlyPaymentAmount;

                item.Person = current.Person;
                _context.SaveChanges();
            }
        }

        public bool ValidateItem(Account item) => item != null && item.Person != null && item.PersonID > 0;
    }
}
