using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DataCache.Models.Mapping
{
    public class PrivilegeGroupsMap : EntityTypeConfiguration<PrivilegesGroup>
    {
        public PrivilegeGroupsMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID);

            this.Property(t => t.GroupName)
                .HasMaxLength(45);

            // Table & Column Mappings
            this.ToTable("privilege_groups", "zera_levi");
            this.Property(t => t.ID).HasColumnName("_id");
            this.Property(t => t.GroupName).HasColumnName("GroupName");

            // Relationships
            this.HasMany(t => t.Privileges)
                .WithMany(t => t.PrivilegesGroup)
                .Map(m =>
                    {
                        m.ToTable("privileges_per_group", "zera_levi");
                        m.MapLeftKey("group_id");
                        m.MapRightKey("privilege_id");
                    });


        }
    }
}
