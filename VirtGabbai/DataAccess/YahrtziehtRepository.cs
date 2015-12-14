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
    public class YahrtziehtRepository : IRepository<Yahrtzieht>
    {
        private ZeraLeviContext _context;

        public DbSet<Yahrtzieht> Entities => _context.Yahrtziehts;

        public YahrtziehtRepository(ZeraLeviContext context)
        {
            _context = context;
        }

        public bool Exists(Yahrtzieht item) => item != null && Exists(item.ID);

        public bool Exists(int id) => Entities.Any(y => y.ID == id);

        public IEnumerable<Yahrtzieht> Get() => Entities;

        public Yahrtzieht GetByID(int id) => Entities.FirstOrDefault(y => y.ID == id);

        public void Add(Yahrtzieht item)
        {
            if (item != null)
            {
                Entities.Add(item);
                _context.SaveChanges();
            }
        }

        public void Delete(Yahrtzieht item)
        {
            if (Exists(item))
            {
                Entities.Remove(item);
                _context.SaveChanges();
            }
        }

        public void Save(Yahrtzieht item)
        {
            if (item != null)
            {
                var current = GetByID(item.ID);

                if (current == null)
                {
                    current = new Yahrtzieht();
                    Entities.Add(current);
                }

                current.Date = item.Date;
                current.Name = item.Name;
                current.Relation = item.Relation;
                _context.SaveChanges();
            }
        }
    }
}
