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
    public class DonationRepository : IRepository<Donation>
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

        public void Add(Donation item)
        {
            if (item != null)
            {
                Entities.Add(item);
                _context.SaveChanges();
            }
        }

        public void Delete(Donation item)
        {
            if (Exists(item))
            {
                Entities.Remove(item);
                _context.SaveChanges();
            }
        }

        public void Save(Donation item)
        {
            var current = GetByID(item.ID);

            if (current == null)
            {
                current = new Donation();
                Entities.Add(current);
            }

            current.Amount = item.Amount;
            current.Comments = item.Comments;
            current.DatePaid = item.DatePaid;
            current.DonationDate = item.DonationDate;
            current.Paid = item.Paid;
            current.Reason = item.Reason;

            _context.SaveChanges(); 
        }
    }
}
