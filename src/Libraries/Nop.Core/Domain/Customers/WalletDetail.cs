using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc.Internal;

namespace Nop.Core.Domain.Customers
{
    public partial class WalletDetail : BaseEntity
    {
        public int WalletID { get; set; } 
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }

        public decimal Amount { get; set; }
        public bool AmountAdded { get; set; }

        public bool AmountDeducted { get; set; }

        public bool IsActive { get; set; }

        public virtual Wallet Wallet { get; set; }
    }
}
