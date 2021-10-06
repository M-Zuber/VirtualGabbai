using DataCache.Models;
using System.Data.Entity.ModelConfiguration;

namespace DataCache.Mapping
{
    public class PeopleMap : EntityTypeConfiguration<Person>
    {
        public PeopleMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.Id);

            Property(t => t.Email)
                .HasMaxLength(45);

            Property(t => t.GivenName)
                .HasMaxLength(45);

            Property(t => t.FamilyName)
                .HasMaxLength(45);

            Property(t => t.Address)
                .HasMaxLength(300);

            // Table & Column Mappings
            ToTable("People", "ZeraLevi");
            Property(t => t.Id).HasColumnName("ID");
            Property(t => t.Email).HasColumnName("Email");
            Property(t => t.GivenName).HasColumnName("GivenName");
            Property(t => t.FamilyName).HasColumnName("FamilyName");
            Property(t => t.Address).HasColumnName("Address");
            Property(t => t.Member).HasColumnName("Member");
        }
    }
}
