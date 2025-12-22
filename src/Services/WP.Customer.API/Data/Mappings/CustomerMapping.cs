using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WP.Core.DomainObjects;
using WP.Customer.API.Models;

namespace WP.Customer.API.Data.Mappings
{
    public class CustomerMapping : IEntityTypeConfiguration<Models.Customer>
    {
        public void Configure(EntityTypeBuilder<Models.Customer> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.OwnsOne(c => c.Email, email =>
            {
                email.Property(e => e.EmailAddress)
                    .IsRequired()
                    .HasColumnName("Email")
                    .HasColumnType($"varchar({Email.EmailMaxLength})");
            });

            // 1 : 1 => Customer : Address
            builder.HasOne(c => c.Address)
                .WithOne(a => a.Customer)
                .HasForeignKey<Address>(a => a.CustomerId);

            builder.ToTable("Customers");
        }
    }
}
