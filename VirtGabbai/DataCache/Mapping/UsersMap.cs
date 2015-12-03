using DataCache.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DataCache.Mapping
{
    public class UsersMap : EntityTypeConfiguration<User>
    {
        public UsersMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID);

            this.Property(t => t.UserName)
                .IsRequired()
                .HasMaxLength(45);

            this.Property(t => t.Password)
                .IsRequired()
                .HasMaxLength(45);

            this.Property(t => t.Email)
                .IsRequired()
                .HasMaxLength(45);

            // Table & Column Mappings
            this.ToTable("Users", "ZeraLevi");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.UserName).HasColumnName("UserName");
            this.Property(t => t.Password).HasColumnName("Password");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.PrivilegesGroupID).HasColumnName("PrivilegesGroupID");

            // Relationships
            this.HasRequired(t => t.PrivilegeGroup)
                .WithMany(t => t.Users)
                .HasForeignKey(d => d.PrivilegesGroupID);

        }
    }
}
