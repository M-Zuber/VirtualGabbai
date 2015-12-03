using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DataCache.Models.Mapping
{
    public class AccountsMap : EntityTypeConfiguration<Account>
    {
        public AccountsMap()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            Property(t => t.ID);

            // Table & Column Mappings
            ToTable("accounts", "zera_levi");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.PersonID).HasColumnName("PersonID");
            Property(t => t.LastMonthlyPaymentDate).HasColumnName("LastMonthlyPaymentDate");

            // Relationships
            HasRequired(t => t.Person)
                .WithOptional(d => d.Account);
        }
    }
}
