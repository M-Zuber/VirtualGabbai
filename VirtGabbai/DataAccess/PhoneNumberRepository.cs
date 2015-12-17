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
    public class PhoneNumberRepository : IFullAccessRepository<PhoneNumber>
    {
        private ZeraLeviContext _context;

        public DbSet<PhoneNumber> Entities => _context.PhoneNumbers;

        public PhoneNumberRepository(ZeraLeviContext context)
        {
            _context = context;            
        }

        public bool Exists(PhoneNumber item) => item != null && Exists(item.ID);

        public bool Exists(int id) => Entities.Any(pn => pn.ID == id);

        public IEnumerable<PhoneNumber> Get() => Entities;

        public PhoneNumber GetByID(int id) => Entities.FirstOrDefault(pn => pn.ID == id);

        public void Add(PhoneNumber item)
        {
            if (item != null)
            {
                Entities.Add(item);
                _context.SaveChanges();
            }
        }

        public void Delete(PhoneNumber item)
        {
            if (Exists(item))
            {
                Entities.Remove(item);
                _context.SaveChanges();
            }
        }

        public void Save(PhoneNumber item)
        {
            if (item != null)
            {
                var current = GetByID(item.ID);

                if (current == null)
                {
                    current = new PhoneNumber();
                    Entities.Add(current);
                }

                current.Number = item.Number;
                current.Type = item.Type;
                _context.SaveChanges();
            }
        }
    }
}
