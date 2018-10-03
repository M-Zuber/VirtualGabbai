using System.Data.Entity;
using DataCache.Mapping;

namespace DataCache.Models
{
    public class ZeraLeviContext : DbContext
    {
        public ZeraLeviContext()
            : this("Name=VGContext")
        {
        }

        public ZeraLeviContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Donation> Donations { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<PhoneNumber> PhoneNumbers { get; set; }
        public virtual DbSet<PhoneType> PhoneTypes { get; set; }
        public virtual DbSet<PrivilegesGroup> PrivilegesGroups { get; set; }
        public virtual DbSet<Privilege> Privileges { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Yahrtzieht> Yahrtziehts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new DonationsMap());
            modelBuilder.Configurations.Add(new AccountsMap());
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
