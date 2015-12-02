using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DataCache.Models.Mapping
{
    public class t_peopleMap : EntityTypeConfiguration<t_people>
    {
        public t_peopleMap()
        {
            // Primary Key
            this.HasKey(t => t.C_id);

            // Properties
            this.Property(t => t.C_id);

            this.Property(t => t.email)
                .HasMaxLength(45);

            this.Property(t => t.given_name)
                .HasMaxLength(45);

            this.Property(t => t.family_name)
                .HasMaxLength(45);

            this.Property(t => t.address)
                .HasMaxLength(300);

            // Table & Column Mappings
            this.ToTable("people", "zera_levi");
            this.Property(t => t.C_id).HasColumnName("_id");
            this.Property(t => t.email).HasColumnName("email");
            this.Property(t => t.given_name).HasColumnName("given_name");
            this.Property(t => t.family_name).HasColumnName("family_name");
            this.Property(t => t.address).HasColumnName("address");
            this.Property(t => t.member).HasColumnName("member");
        }
    }
}
