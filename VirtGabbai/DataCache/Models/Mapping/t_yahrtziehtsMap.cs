using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DataCache.Models.Mapping
{
    public class t_yahrtziehtsMap : EntityTypeConfiguration<t_yahrtziehts>
    {
        public t_yahrtziehtsMap()
        {
            // Primary Key
            this.HasKey(t => t.C_id);

            // Properties
            this.Property(t => t.C_id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.relation)
                .HasMaxLength(45);

            this.Property(t => t.deceaseds_name)
                .IsRequired()
                .HasMaxLength(45);

            // Table & Column Mappings
            this.ToTable("t_yahrtziehts", "zera_levi");
            this.Property(t => t.C_id).HasColumnName("_id");
            this.Property(t => t.person_id).HasColumnName("person_id");
            this.Property(t => t.relation).HasColumnName("relation");
            this.Property(t => t.date).HasColumnName("date");
            this.Property(t => t.deceaseds_name).HasColumnName("deceaseds_name");

            // Relationships
            this.HasRequired(t => t.t_people)
                .WithMany(t => t.t_yahrtziehts)
                .HasForeignKey(d => d.person_id);

        }
    }
}
