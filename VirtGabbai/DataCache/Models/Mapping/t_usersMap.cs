using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DataCache.Models.Mapping
{
    public class t_usersMap : EntityTypeConfiguration<t_users>
    {
        public t_usersMap()
        {
            // Primary Key
            this.HasKey(t => t.C_id);

            // Properties
            this.Property(t => t.C_id);

            this.Property(t => t.name)
                .IsRequired()
                .HasMaxLength(45);

            this.Property(t => t.password)
                .IsRequired()
                .HasMaxLength(45);

            this.Property(t => t.email)
                .IsRequired()
                .HasMaxLength(45);

            // Table & Column Mappings
            this.ToTable("users", "zera_levi");
            this.Property(t => t.C_id).HasColumnName("_id");
            this.Property(t => t.name).HasColumnName("name");
            this.Property(t => t.password).HasColumnName("password");
            this.Property(t => t.email).HasColumnName("email");
            this.Property(t => t.privileges_group).HasColumnName("privileges_group");

            // Relationships
            this.HasRequired(t => t.t_privilege_groups)
                .WithMany(t => t.t_users)
                .HasForeignKey(d => d.privileges_group);

        }
    }
}
