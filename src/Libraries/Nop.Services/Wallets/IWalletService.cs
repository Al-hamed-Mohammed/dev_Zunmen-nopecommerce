using System;
using System.Collections.Generic;
using System.Text;
using Nop.Core;
using Nop.Core.Domain.Customers;

namespace Nop.Services.Wallets
{
    public partial interface IWalletService
    {
        /// <summary>
        /// Gets a Wallet by Wallet identifier
        /// </summary>
        /// <param name="WalletId">Wallet identifier</param>
        /// <returns>Wallet</returns>
        Wallet GetWalletById(int walletId);

        List<Wallet> GetAllWallet();

        /// <summary>
        /// Delete a Wallet
        /// </summary>
        /// <param name="Wallet">Wallet</param>
        void DeleteWallet(Wallet wallet);
               
        
        /// <summary>
        /// Inserts a Wallet
        /// </summary>
        /// <param name="Wallet">Wallet</param>
        void InsertWallet(Wallet wallet);

        /// <summary>
        /// Updates the Wallet
        /// </summary>
        /// <param name="Wallet">Wallet</param>
        void UpdateWallet(Wallet wallet);
    }
}
