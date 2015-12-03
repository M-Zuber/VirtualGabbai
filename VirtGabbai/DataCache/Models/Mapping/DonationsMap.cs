using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DataCache.Models.Mapping
{
    public class DonationsMap : EntityTypeConfiguration<Donation>
    {
        public DonationsMap()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            Property(t => t.ID);

            Property(t => t.Reason)
                .IsRequired()
                .HasMaxLength(100);

            Property(t => t.Comments)
                .HasMaxLength(300);

            // Table & Column Mappings
            ToTable("Donations", "zera_levi");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.AccountID).HasColumnName("AccountID");
            Property(t => t.Reason).HasColumnName("Reason");
            Property(t => t.Amount).HasColumnName("Amount");
            Property(t => t.DonationDate).HasColumnName("DonationDate");
            Property(t => t.DatePaid).HasColumnName("DatePaid");
            Property(t => t.Paid).HasColumnName("Paid");
            Property(t => t.Comments).HasColumnName("Comments");

            // Relationships
            HasRequired(t => t.Accounts)
                .WithMany(t => t.Donations)
                .HasForeignKey(d => d.AccountID);

        }
    }
}
