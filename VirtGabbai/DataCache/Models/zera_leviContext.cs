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

        public DbSet<Account> t_accounts { get; set; }
        public DbSet<Donation> t_donations { get; set; }
        public DbSet<Person> t_people { get; set; }
        public DbSet<PhoneNumber> t_phone_numbers { get; set; }
        public DbSet<PhoneType> t_phone_types { get; set; }
        public DbSet<PrivilegesGroup> t_privilege_groups { get; set; }
        public DbSet<Privilege> t_privileges { get; set; }
        public DbSet<User> t_users { get; set; }
        public DbSet<Yahrtzieht> t_yahrtziehts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new AccountsMap());
            modelBuilder.Configurations.Add(new DonationsMap());
            modelBuilder.Configurations.Add(new PeopleMap());
            modelBuilder.Configurations.Add(new PhoneNumbersMap());
            modelBuilder.Configurations.Add(new PhoneTypesMap());
            modelBuilder.Configurations.Add(new PrivilegeGroupsMap());
            modelBuilder.Configurations.Add(new PrivilegesMap());
            modelBuilder.Configurations.Add(new UsersMap());
            modelBuilder.Configurations.Add(new YahrtziehtsMap());
        }
    }
}
