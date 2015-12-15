using System;
using System.Collections.Generic;
using System.Linq;
using DataCache;
using Framework;
using System.Net.Mail;
using DataCache.Models;
using DataAccess.Interfaces;
using System.Data.Entity;

namespace DataAccess
{
    public class PersonRepository : IRepository<Person>
    {
        private ZeraLeviContext _context;

        public DbSet<Person> Entities => _context.People;

        public PersonRepository(ZeraLeviContext context)
        {
            _context = context;
        }

        public bool Exists(Person item) => item != null && Exists(item.ID);

        public bool Exists(int id) => Entities.Any(p => p.ID == id);

        public IEnumerable<Person> Get() => Entities;

        public Person GetByID(int id) => Entities.FirstOrDefault(p => p.ID == id);

        public void Add(Person item)
        {
            if (item != null)
            {
                Entities.Add(item);
                _context.SaveChanges();
            }
        }

        public void Delete(Person item)
        {
            if (Exists(item))
            {
                Entities.Remove(item);
                _context.SaveChanges();
            }
        }

        public void Save(Person item)
        {
            if (item != null)
            {
                var current = GetByID(item.ID);

                if (current == null)
                {
                    current = new Person();
                    Entities.Add(current);
                }

                current.Account = item.Account;
                current.Address = item.Address;
                current.Email = item.Email;
                current.FamilyName = item.FamilyName;
                current.GivenName = item.GivenName;
                current.Member = item.Member;
                current.PhoneNumbers = item.PhoneNumbers;
                current.Yahrtziehts = item.Yahrtziehts;
                _context.SaveChanges();
            }
        }
    }
}
