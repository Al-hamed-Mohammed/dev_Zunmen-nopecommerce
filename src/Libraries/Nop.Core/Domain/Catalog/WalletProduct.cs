using System;
using System.Collections.Generic;
using System.Text;

namespace Nop.Core.Domain.Catalog
{
    /// <summary>
    /// Represents a Wallet Product
    /// </summary>
    public static class WalletProduct
    {
        /// <summary>
        /// Product - Name of the Wallet product add by Admin
        /// </summary>
        public const string PRODUCT = "Balance Voucher";

        public static readonly decimal ProductCharge = 1;
    }
}
