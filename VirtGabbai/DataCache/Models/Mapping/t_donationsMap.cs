using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DataCache.Models.Mapping
{
    public class t_donationsMap : EntityTypeConfiguration<t_donations>
    {
        public t_donationsMap()
        {
            // Primary Key
            this.HasKey(t => t.C_id);

            // Properties
            this.Property(t => t.C_id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.reason)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.comments)
                .HasMaxLength(300);

            // Table & Column Mappings
            this.ToTable("t_donations", "zera_levi");
            this.Property(t => t.C_id).HasColumnName("_id");
            this.Property(t => t.account_id).HasColumnName("account_id");
            this.Property(t => t.reason).HasColumnName("reason");
            this.Property(t => t.amount).HasColumnName("amount");
            this.Property(t => t.date_donated).HasColumnName("date_donated");
            this.Property(t => t.date_paid).HasColumnName("date_paid");
            this.Property(t => t.paid).HasColumnName("paid");
            this.Property(t => t.comments).HasColumnName("comments");

            // Relationships
            this.HasRequired(t => t.t_accounts)
                .WithMany(t => t.t_donations)
                .HasForeignKey(d => d.account_id);

        }
    }
}
