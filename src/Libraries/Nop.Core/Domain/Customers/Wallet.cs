using System;
using System.Collections.Generic;
using System.Text;
using Nop.Core.Domain.Orders;

namespace Nop.Core.Domain.Customers
{
    public partial class Wallet : BaseEntity
    {
       public decimal Amount { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? DateModified { get; set; }

        public int CustomerID { get; set; }
        public bool IsActive { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual ICollection<WalletDetail> walletDetails { get; set; }
    }
}
