using DataCache.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DataCache.Mapping
{
    public class PhoneNumbersMap : EntityTypeConfiguration<PhoneNumber>
    {
        public PhoneNumbersMap()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            Property(t => t.Number)
                .IsRequired()
                .HasMaxLength(45);

            Property(t => t.ID);

            // Table & Column Mappings
            ToTable("PhoneNumbers", "ZeraLevi");
            Property(t => t.PersonID).HasColumnName("PersonID");
            Property(t => t.Number).HasColumnName("Number");
            Property(t => t.NumberTypeID).HasColumnName("NumberTypeID");
            Property(t => t.ID).HasColumnName("ID");

            // Relationships
            HasRequired(t => t.Person)
                .WithMany(t => t.PhoneNumbers)
                .HasForeignKey(d => d.PersonID);
            HasRequired(t => t.Type)
                .WithMany(t => t.PhoneNumbers)
                .HasForeignKey(d => d.NumberTypeID);

        }
    }
}
