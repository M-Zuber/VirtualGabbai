using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using DataCache.Models.Mapping;

namespace DataCache.Models
{
    public partial class zera_leviContext : DbContext
    {
        public zera_leviContext()
            : base("Name=VGContext")
        {
        }

        public DbSet<t_accounts> t_accounts { get; set; }
        public DbSet<t_donations> t_donations { get; set; }
        public DbSet<t_people> t_people { get; set; }
        public DbSet<t_phone_numbers> t_phone_numbers { get; set; }
        public DbSet<t_phone_types> t_phone_types { get; set; }
        public DbSet<t_privilege_groups> t_privilege_groups { get; set; }
        public DbSet<t_zl_privileges> t_privileges { get; set; }
        public DbSet<t_users> t_users { get; set; }
        public DbSet<t_yahrtziehts> t_yahrtziehts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new t_accountsMap());
            modelBuilder.Configurations.Add(new t_donationsMap());
            modelBuilder.Configurations.Add(new t_peopleMap());
            modelBuilder.Configurations.Add(new t_phone_numbersMap());
            modelBuilder.Configurations.Add(new t_phone_typesMap());
            modelBuilder.Configurations.Add(new t_privilege_groupsMap());
            modelBuilder.Configurations.Add(new t_privilegesMap());
            modelBuilder.Configurations.Add(new t_usersMap());
            modelBuilder.Configurations.Add(new t_yahrtziehtsMap());
        }
    }
}
