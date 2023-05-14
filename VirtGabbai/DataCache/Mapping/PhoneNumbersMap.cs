using DataCache.Models;
using System.Data.Entity.ModelConfiguration;

namespace DataCache.Mapping
{
    public class PhoneNumbersMap : EntityTypeConfiguration<PhoneNumber>
    {
        public PhoneNumbersMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.Number)
                .IsRequired()
                .HasMaxLength(45);

            Property(t => t.Id);

            // Table & Column Mappings
            ToTable("PhoneNumbers", "ZeraLevi");
            Property(t => t.PersonId).HasColumnName("PersonID");
            Property(t => t.Number).HasColumnName("Number");
            Property(t => t.Id).HasColumnName("ID");

            // Relationships
            HasRequired(t => t.Person)
                .WithMany(t => t.PhoneNumbers)
                .HasForeignKey(d => d.PersonId);
        }
    }
}
