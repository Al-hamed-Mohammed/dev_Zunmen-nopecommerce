using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Security.Cryptography;
using System.Text;
using Nop.Core.Data;
using Nop.Core.Domain.Customers;
using Nop.Services.Events;

namespace Nop.Services.Wallets
{
    public partial class WalletService : IWalletService
    {
        #region Fields
        private readonly IEventPublisher _eventPublisher;
        private readonly IRepository<Wallet> _walletRepository;
        #endregion

        #region Ctor
        public WalletService(IEventPublisher eventPublisher, IRepository<Wallet> walletRepository)
        {
            _eventPublisher = eventPublisher;
            _walletRepository = walletRepository;
        }

        #endregion

        #region Methods
        public void DeleteWallet(Wallet wallet)
        {
            if (wallet == null)
                throw new ArgumentNullException(nameof(wallet));

            wallet.IsActive = false;
            UpdateWallet(wallet);

            //event notification
            _eventPublisher.EntityDeleted(wallet);
        }

        public List<Wallet> GetAllWallet()
        {
            var query = _walletRepository.Table;
            return query.ToList();
        }

        public Wallet GetWalletById(int walletId)
        {
            if (walletId == 0)
                return null;

            return _walletRepository.GetById(walletId);
        }

        public void InsertWallet(Wallet wallet)
        {
            if (wallet == null)
                throw new ArgumentNullException(nameof(wallet));

            _walletRepository.Insert(wallet);

            //event notification
            _eventPublisher.EntityInserted(wallet);
        }

        public void UpdateWallet(Wallet wallet)
        {
            if (wallet == null)
                throw new ArgumentNullException(nameof(wallet));

            _walletRepository.Update(wallet);

            //event notification
            _eventPublisher.EntityUpdated(wallet);
        }

        #endregion
    }
}
