using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DataCache.Models.Mapping
{
    public class t_phone_typesMap : EntityTypeConfiguration<t_phone_types>
    {
        public t_phone_typesMap()
        {
            // Primary Key
            this.HasKey(t => t.C_id);

            // Properties
            this.Property(t => t.C_id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.type_name)
                .IsRequired()
                .HasMaxLength(45);

            // Table & Column Mappings
            this.ToTable("t_phone_types", "zera_levi");
            this.Property(t => t.C_id).HasColumnName("_id");
            this.Property(t => t.type_name).HasColumnName("type_name");
        }
    }
}
