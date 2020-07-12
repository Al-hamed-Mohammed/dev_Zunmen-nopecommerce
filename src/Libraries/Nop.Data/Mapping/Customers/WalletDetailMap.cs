using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nop.Core.Domain.Customers;

namespace Nop.Data.Mapping.Customers
{
    public partial class WalletDetailMap : NopEntityTypeConfiguration<WalletDetail>
    {
        public override void Configure(EntityTypeBuilder<WalletDetail> builder)
        {
            builder.HasKey(w => w.Id);
            builder.Property(w => w.Id)
                .UseSqlServerIdentityColumn();

            builder.Property(w => w.Description)
                .HasColumnType("nvarchar")
                .HasMaxLength(1000);

            builder.HasOne(w => w.Wallet)
                .WithMany(wd => wd.walletDetails)
                .HasForeignKey(w => w.WalletID);


            base.Configure(builder);
        }
    }
}
