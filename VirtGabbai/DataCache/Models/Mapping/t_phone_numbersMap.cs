using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DataCache.Models.Mapping
{
    public class t_phone_numbersMap : EntityTypeConfiguration<PhoneNumber>
    {
        public t_phone_numbersMap()
        {
            // Primary Key
            this.HasKey(t => t.C_id);

            // Properties
            this.Property(t => t.number)
                .IsRequired()
                .HasMaxLength(45);

            this.Property(t => t.C_id);

            // Table & Column Mappings
            this.ToTable("phone_numbers", "zera_levi");
            this.Property(t => t.person_id).HasColumnName("person_id");
            this.Property(t => t.number).HasColumnName("number");
            this.Property(t => t.number_type).HasColumnName("number_type");
            this.Property(t => t.C_id).HasColumnName("_id");

            // Relationships
            this.HasRequired(t => t.t_people)
                .WithMany(t => t.PhoneNumbers)
                .HasForeignKey(d => d.person_id);
            this.HasRequired(t => t.t_phone_types)
                .WithMany(t => t.t_phone_numbers)
                .HasForeignKey(d => d.number_type);

        }
    }
}
