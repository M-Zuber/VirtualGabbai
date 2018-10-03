using DataCache.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace DataCache.Mapping
{
    public class PrivilegesMap : EntityTypeConfiguration<Privilege>
    {
        public PrivilegesMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.Id);

            Property(t => t.Name)
                .HasMaxLength(45);

            // Table & Column Mappings
            ToTable("Privileges", "ZeraLevi");
            Property(t => t.Id).HasColumnName("ID");
            Property(t => t.Name)
                .HasColumnName("Name")
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                            new IndexAnnotation(new IndexAttribute("IX_Privileges_Name") { IsUnique = true }));
        }
    }
}
