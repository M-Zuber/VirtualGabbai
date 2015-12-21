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
    public class UserRepository : IFullAccessRepository<User>
    {
        private ZeraLeviContext _context;

        public DbSet<User> Entities => _context.Users;

        public UserRepository(ZeraLeviContext context)
        {
            _context = context;
        }

        public bool Exists(User item) => item != null && Exists(item.ID);

        public bool Exists(int id) => Entities.Any(u => u.ID == id);

        public IEnumerable<User> Get() => Entities;

        public User GetByID(int id) => Entities.FirstOrDefault(u => u.ID == id);

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
                var current = GetByID(item.ID);

                if (current == null)
                {
                    current = new User();
                    current.Email = item.Email;
                    current.Password = item.Password;
                    current.PrivilegeGroup = item.PrivilegeGroup;
                    current.UserName = item.UserName;
                    Entities.Add(current);
                }

                _context.SaveChanges();

                item.ID = current.ID;
            }
        }
    }
}
