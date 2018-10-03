using DataAccess.Interfaces;
using DataCache.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataAccess
{
    public class UserRepository : IFullAccessRepository<User>
    {
        private readonly ZeraLeviContext _context;

        public DbSet<User> Entities => _context.Users;

        public UserRepository(ZeraLeviContext context)
        {
            _context = context;
        }

        public bool Exists(User item) => item != null && Exists(item.Id);

        public bool Exists(int id) => Entities.Any(u => u.Id == id);

        public IEnumerable<User> Get() => Entities;

        public User GetById(int id) => Entities.FirstOrDefault(u => u.Id == id);

        public void Add(User item)
        {
            if (item != null)
            {
                Entities.Add(item);
                _context.SaveChanges();
            }
        }

        public void Delete(User item)
        {
            if (Exists(item))
            {
                Entities.Remove(item);
                _context.SaveChanges();
            }
        }

        public void Save(User item)
        {
            if (item != null)
            {
                var current = GetById(item.Id);

                if (current == null)
                {
                    current = new User
                    {
                        Email = item.Email,
                        Password = item.Password,
                        PrivilegeGroup = item.PrivilegeGroup,
                        UserName = item.UserName
                    };
                    Entities.Add(current);
                }

                _context.SaveChanges();

                item.Id = current.Id;
            }
        }
    }
}
