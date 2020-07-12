using System;
using System.Collections.Generic;
using System.Text;
using Nop.Core.Domain.Customers;


namespace Nop.Services.Wallets
{
    public partial interface IWalletDetailService
    {
        /// <summary>
        /// Gets a Wallet by Wallet identifier
        /// </summary>
        /// <param name="WalletId">Wallet identifier</param>
        /// <returns>Wallet</returns>
        WalletDetail GetWalletDetailById(int id);

        List<WalletDetail> GetAllWalletDetail();

        /// <summary>
        /// Delete a Wallet
        /// </summary>
        /// <param name="Wallet">Wallet</param>
        void DeleteWalletDetail(WalletDetail walletDetail);


        /// <summary>
        /// Inserts a Wallet
        /// </summary>
        /// <param name="Wallet">Wallet</param>
        void InsertWalletDetail(WalletDetail walletDetail);

        /// <summary>
        /// Updates the Wallet
        /// </summary>
        /// <param name="Wallet">Wallet</param>
        void UpdateWalletDetail(WalletDetail walletDetail);
    }
}
