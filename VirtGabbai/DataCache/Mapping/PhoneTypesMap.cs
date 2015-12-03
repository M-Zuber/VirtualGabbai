using DataCache.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DataCache.Mapping
{
    public class PhoneTypesMap : EntityTypeConfiguration<PhoneType>
    {
        public PhoneTypesMap()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            Property(t => t.ID);

            Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(45);

            // Table & Column Mappings
            ToTable("PhoneTypes", "zera_levi");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.Name).HasColumnName("Name");
        }
    }
}
