using DataCache.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DataCache.Mapping
{
    public class YahrtziehtsMap : EntityTypeConfiguration<Yahrtzieht>
    {
        public YahrtziehtsMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID);

            this.Property(t => t.Relation)
                .HasMaxLength(45);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(45);

            // Table & Column Mappings
            this.ToTable("Yahrtziehts", "ZeraLevi");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.PersonID).HasColumnName("PersonID");
            this.Property(t => t.Relation).HasColumnName("Relation");
            this.Property(t => t.Date).HasColumnName("Date").HasColumnType("datetime2");
            this.Property(t => t.Name).HasColumnName("Name");

            // Relationships
            this.HasRequired(t => t.Person)
                .WithMany(t => t.Yahrtziehts)
                .HasForeignKey(d => d.PersonID);

        }
    }
}
