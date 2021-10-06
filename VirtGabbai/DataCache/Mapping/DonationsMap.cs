using DataCache.Models;
using System.Data.Entity.ModelConfiguration;

namespace DataCache.Mapping
{
    public class DonationsMap : EntityTypeConfiguration<Donation>
    {
        public DonationsMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.Id);

            Property(t => t.Reason)
                .IsRequired()
                .HasMaxLength(100);

            Property(t => t.Comments)
                .HasMaxLength(300);

            // Table & Column Mappings
            ToTable("Donations", "ZeraLevi");
            Property(t => t.Id).HasColumnName("ID");
            Property(t => t.AccountId).HasColumnName("AccountID");
            Property(t => t.Reason).HasColumnName("Reason");
            Property(t => t.Amount).HasColumnName("Amount");
            Property(t => t.DonationDate).HasColumnName("DonationDate").HasColumnType("datetime2");
            Property(t => t.DatePaid).HasColumnName("DatePaid").HasColumnType("datetime2");
            Property(t => t.Paid).HasColumnName("Paid");
            Property(t => t.Comments).HasColumnName("Comments");
        }
    }
}
