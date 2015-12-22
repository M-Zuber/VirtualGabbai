using DataCache.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DataCache.Mapping
{
    public class AccountsMap : EntityTypeConfiguration<Account>
    {
        public AccountsMap()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            Property(t => t.ID);

            Ignore(t => t.PaidDonations);
            Ignore(t => t.UnpaidDonations);

            // Table & Column Mappings
            ToTable("Accounts", "ZeraLevi");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.PersonID).HasColumnName("PersonID");
            Property(t => t.LastMonthlyPaymentDate).HasColumnName("LastMonthlyPaymentDate").HasColumnType("datetime2");
            Property(t => t.MonthlyPaymentAmount).HasColumnName("MonthlyPaymentAmount");

            // Relationships
            HasRequired(t => t.Person)
                .WithOptional(d => d.Account);
        }
    }
}