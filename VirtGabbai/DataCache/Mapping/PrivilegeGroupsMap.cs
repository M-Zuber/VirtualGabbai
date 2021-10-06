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
            HasKey(t => t.Id);

            // Properties
            Property(t => t.Id);

            Property(t => t.GroupName)
                .HasMaxLength(45);

            // Table & Column Mappings
            ToTable("PrivilegesGroup", "ZeraLevi");
            Property(t => t.Id).HasColumnName("ID");
            Property(t => t.GroupName)
                .HasColumnName("GroupName")
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                            new IndexAnnotation(new IndexAttribute("IX_PrivilegeGroups_GroupName") { IsUnique = true }));

            // Relationships
            HasMany(t => t.Privileges)
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
