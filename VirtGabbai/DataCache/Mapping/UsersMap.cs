using DataCache.Models;
using System.Data.Entity.ModelConfiguration;

namespace DataCache.Mapping
{
    public class UsersMap : EntityTypeConfiguration<User>
    {
        public UsersMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.Id);

            Property(t => t.UserName)
                .IsRequired()
                .HasMaxLength(45);

            Property(t => t.Password)
                .IsRequired()
                .HasMaxLength(45);

            Property(t => t.Email)
                .IsRequired()
                .HasMaxLength(45);

            // Table & Column Mappings
            ToTable("Users", "ZeraLevi");
            Property(t => t.Id).HasColumnName("ID");
            Property(t => t.UserName).HasColumnName("UserName");
            Property(t => t.Password).HasColumnName("Password");
            Property(t => t.Email).HasColumnName("Email");
            Property(t => t.PrivilegesGroupId).HasColumnName("PrivilegesGroupID");

            // Relationships
            HasRequired(t => t.PrivilegeGroup)
                .WithMany(t => t.Users)
                .HasForeignKey(d => d.PrivilegesGroupId);
        }
    }
}
