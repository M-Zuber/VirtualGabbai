using DataCache.Models;
using System.Data.Entity.ModelConfiguration;

namespace DataCache.Mapping
{
    public class AccountsMap : EntityTypeConfiguration<Account>
    {
        public AccountsMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.Id);

            Ignore(t => t.PaidDonations);
            Ignore(t => t.UnpaidDonations);

            // Table & Column Mappings
            ToTable("Accounts", "ZeraLevi");
            Property(t => t.Id).HasColumnName("ID");
            Property(t => t.PersonId).HasColumnName("PersonID");
            Property(t => t.LastMonthlyPaymentDate).HasColumnName("LastMonthlyPaymentDate").HasColumnType("datetime2");
            Property(t => t.MonthlyPaymentAmount).HasColumnName("MonthlyPaymentAmount");

            // Relationships
            HasRequired(t => t.Person)
                .WithOptional(d => d.Account);
        }
    }
}