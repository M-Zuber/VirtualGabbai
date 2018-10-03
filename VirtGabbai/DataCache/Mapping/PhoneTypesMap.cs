using DataCache.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace DataCache.Mapping
{
    public class PhoneTypesMap : EntityTypeConfiguration<PhoneType>
    {
        public PhoneTypesMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.Id);

            Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(45);

            // Table & Column Mappings
            ToTable("PhoneTypes", "ZeraLevi");
            Property(t => t.Id).HasColumnName("ID");
            Property(t => t.Name)
                .HasColumnName("Name")
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                            new IndexAnnotation(new IndexAttribute("IX_PhoneType_Name") { IsUnique = true }));
        }
    }
}
