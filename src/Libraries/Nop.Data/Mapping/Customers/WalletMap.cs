using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nop.Core.Domain.Customers;

namespace Nop.Data.Mapping.Customers
{
    public partial class WalletMap: NopEntityTypeConfiguration<Wallet>
    {
        public override void Configure(EntityTypeBuilder<Wallet> builder)
        {
            builder.ToTable(nameof(Wallet));
            builder.HasKey(wallet => wallet.Id);
            builder.Property(wallet => wallet.Id)
                .UseSqlServerIdentityColumn();

            builder.HasOne(wallet => wallet.Customer)
                .WithOne(customer => customer.Wallet)
                .HasForeignKey<Wallet>(wallet => wallet.CustomerID);

            base.Configure(builder);    
        }
    }
}
