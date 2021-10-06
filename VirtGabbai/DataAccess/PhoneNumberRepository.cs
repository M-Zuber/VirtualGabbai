using DataAccess.Interfaces;
using DataCache.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataAccess
{
    public class PhoneNumberRepository : IReadOnlyRepository<PhoneNumber>
    {
        private readonly ZeraLeviContext _context;

        public DbSet<PhoneNumber> Entities => _context.PhoneNumbers;

        public PhoneNumberRepository(ZeraLeviContext context)
        {
            _context = context;
        }

        public bool Exists(PhoneNumber item) => item != null && Exists(item.Id);

        public bool Exists(int id) => Entities.Any(pn => pn.Id == id);

        public IEnumerable<PhoneNumber> Get() => Entities;

        public PhoneNumber GetById(int id) => Entities.FirstOrDefault(pn => pn.Id == id);
    }
}
