using DataCache.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace DataCache.Mapping
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
            this.ToTable("PrivilegesGroup", "ZeraLevi");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.GroupName)
                .HasColumnName("GroupName")
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                            new IndexAnnotation(new IndexAttribute("IX_PrivilegeGroups_GroupName") { IsUnique = true }));

            // Relationships
            this.HasMany(t => t.Privileges)
                .WithMany(t => t.PrivilegesGroup)
                .Map(m =>
                    {
                        m.ToTable("PrivilegesPerGroup", "ZeraLevi");
                        m.MapLeftKey("GroupID");
                        m.MapRightKey("PrivilegeId");
                    });


        }
    }
}
