using DataAccess.Interfaces;
using DataCache.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataAccess
{
    public class DonationRepository : IReadOnlyRepository<Donation>
    {
        private readonly ZeraLeviContext _context;

        public DbSet<Donation> Entities => _context.Donations;

        public DonationRepository(ZeraLeviContext context)
        {
            _context = context;
        }

        public bool Exists(Donation item) => item != null && Exists(item.Id);

        public bool Exists(int id) => Entities.Any(d => d.Id == id);

        public IEnumerable<Donation> Get() => Entities;

        public Donation GetById(int id) => Entities.FirstOrDefault(d => d.Id == id);
    }
}
