using DataCache.Models;
using System.Data.Entity.ModelConfiguration;

namespace DataCache.Mapping
{
    public class YahrtziehtsMap : EntityTypeConfiguration<Yahrtzieht>
    {
        public YahrtziehtsMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.Id);

            Property(t => t.Relation)
                .HasMaxLength(45);

            Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(45);

            // Table & Column Mappings
            ToTable("Yahrtziehts", "ZeraLevi");
            Property(t => t.Id).HasColumnName("ID");
            Property(t => t.PersonId).HasColumnName("PersonID");
            Property(t => t.Relation).HasColumnName("Relation");
            Property(t => t.Date).HasColumnName("Date").HasColumnType("datetime2");
            Property(t => t.Name).HasColumnName("Name");

            // Relationships
            HasRequired(t => t.Person)
                .WithMany(t => t.Yahrtziehts)
                .HasForeignKey(d => d.PersonId);
        }
    }
}
