using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DataCache.Models.Mapping
{
    public class t_accountsMap : EntityTypeConfiguration<t_accounts>
    {
        public t_accountsMap()
        {
            // Primary Key
            this.HasKey(t => t.C_id);

            // Properties
            this.Property(t => t.C_id);

            // Table & Column Mappings
            this.ToTable("accounts", "zera_levi");
            this.Property(t => t.C_id).HasColumnName("_id");
            this.Property(t => t.person_id).HasColumnName("person_id");
            this.Property(t => t.monthly_total).HasColumnName("monthly_total");
            this.Property(t => t.last_month_paid).HasColumnName("last_month_paid");

            // Relationships
            this.HasRequired(t => t.t_people)
                .WithMany(t => t.t_accounts)
                .HasForeignKey(d => d.person_id);

        }
    }
}
