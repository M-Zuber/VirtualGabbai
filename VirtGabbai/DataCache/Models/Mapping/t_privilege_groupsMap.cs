using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DataCache.Models.Mapping
{
    public class t_privilege_groupsMap : EntityTypeConfiguration<t_privilege_groups>
    {
        public t_privilege_groupsMap()
        {
            // Primary Key
            this.HasKey(t => t.C_id);

            // Properties
            this.Property(t => t.C_id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.group_name)
                .HasMaxLength(45);

            // Table & Column Mappings
            this.ToTable("t_privilege_groups", "zera_levi");
            this.Property(t => t.C_id).HasColumnName("_id");
            this.Property(t => t.group_name).HasColumnName("group_name");

            // Relationships
            this.HasMany(t => t.t_privileges)
                .WithMany(t => t.t_privilege_groups)
                .Map(m =>
                    {
                        m.ToTable("t_privileges_per_group", "zera_levi");
                        m.MapLeftKey("group_id");
                        m.MapRightKey("privilege_id");
                    });


        }
    }
}
