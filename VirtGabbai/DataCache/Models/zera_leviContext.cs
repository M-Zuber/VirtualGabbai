using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using DataCache.Mapping;

namespace DataCache.Models
{
    public partial class zera_leviContext : DbContext
    {
        public zera_leviContext()
            : base("Name=VGContext")
        {
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Donation> Donations { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<PhoneNumber> PhoneNumbers { get; set; }
        public DbSet<PhoneType> PhoneTypes { get; set; }
        public DbSet<PrivilegesGroup> PrivilegesGroups { get; set; }
        public DbSet<Privilege> Privileges { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Yahrtzieht> Yahrtziehts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
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
