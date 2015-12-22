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
    public class DonationRepository : IReadOnlyRepository<Donation>
    {
        private ZeraLeviContext _context;

        public DbSet<Donation> Entities => _context.Donations;

        public DonationRepository(ZeraLeviContext context)
        {
            _context = context;
        }

        public bool Exists(Donation item) => item != null && Exists(item.ID);

        public bool Exists(int id) => Entities.Any(d => d.ID == id);

        public IEnumerable<Donation> Get() => Entities;

        public Donation GetByID(int id) => Entities.FirstOrDefault(d => d.ID == id);
    }
}
