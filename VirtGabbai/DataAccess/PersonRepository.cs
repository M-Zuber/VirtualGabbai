using DataAccess.Interfaces;
using DataCache.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataAccess
{
    public class PersonRepository : IFullAccessRepository<Person>
    {
        private readonly ZeraLeviContext _context;

        public DbSet<Person> Entities => _context.People;

        public PersonRepository(ZeraLeviContext context)
        {
            _context = context;
        }

        public bool Exists(Person item) => item != null && Exists(item.Id);

        public bool Exists(int id) => Entities.Any(p => p.Id == id);

        public IEnumerable<Person> Get() => Entities;

        public Person GetById(int id) => Entities.FirstOrDefault(p => p.Id == id);

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
                var current = GetById(item.Id);

                if (current == null)
                {
                    current = new Person
                    {
                        Account = item.Account,
                        Address = item.Address,
                        Email = item.Email,
                        FamilyName = item.FamilyName,
                        GivenName = item.GivenName,
                        Member = item.Member,
                        PhoneNumbers = item.PhoneNumbers,
                        Yahrtziehts = item.Yahrtziehts
                    };
                    Entities.Add(current);
                }

                _context.SaveChanges();

                item.Id = current.Id;
            }
        }
    }
}
