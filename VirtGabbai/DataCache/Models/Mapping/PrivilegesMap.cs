using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DataCache.Models.Mapping
{
    public class PrivilegesMap : EntityTypeConfiguration<Privilege>
    {
        public PrivilegesMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID);

            this.Property(t => t.Name)
                .HasMaxLength(45);

            // Table & Column Mappings
            this.ToTable("Privileges", "zera_levi");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.Name).HasColumnName("Name");
        }
    }
}
