using DataAccess.Interfaces;
using DataCache.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataAccess
{
    public class YahrtziehtRepository : IReadOnlyRepository<Yahrtzieht>
    {
        private readonly ZeraLeviContext _context;

        public DbSet<Yahrtzieht> Entities => _context.Yahrtziehts;

        public YahrtziehtRepository(ZeraLeviContext context)
        {
            _context = context;
        }

        public bool Exists(Yahrtzieht item) => item != null && Exists(item.Id);

        public bool Exists(int id) => Entities.Any(y => y.Id == id);

        public IEnumerable<Yahrtzieht> Get() => Entities;

        public Yahrtzieht GetById(int id) => Entities.FirstOrDefault(y => y.Id == id);
    }
}
