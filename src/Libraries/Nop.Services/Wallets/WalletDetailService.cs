using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nop.Core.Data;
using Nop.Core.Domain.Customers;
using Nop.Services.Events;

namespace Nop.Services.Wallets
{
    public partial class WalletDetailService : IWalletDetailService
    {
        #region Fields
        private readonly IEventPublisher _eventPublisher;
        private readonly IRepository<WalletDetail> _walletRepository;
        #endregion

        #region Ctor
        public WalletDetailService(IEventPublisher eventPublisher, IRepository<WalletDetail> walletRepository)
        {
            _eventPublisher = eventPublisher;
            _walletRepository = walletRepository;
        }

        #endregion

        #region Methods
        public void DeleteWalletDetail(WalletDetail walletDetail)
        {
            if (walletDetail == null)
                throw new ArgumentNullException(nameof(walletDetail));

            walletDetail.IsActive = false;
            UpdateWalletDetail(walletDetail);

            //event notification
            _eventPublisher.EntityDeleted(walletDetail);
        }

        public List<WalletDetail> GetAllWalletDetail()
        {
            var query = _walletRepository.Table;
            return query.ToList();
        }

        public WalletDetail GetWalletDetailById(int id)
        {
            if (id == 0)
                return null;

            return _walletRepository.GetById(id);
        }

        public void InsertWalletDetail(WalletDetail walletDetail)
        {
            if (walletDetail == null)
                throw new ArgumentNullException(nameof(walletDetail));

            _walletRepository.Insert(walletDetail);

            //event notification
            _eventPublisher.EntityInserted(walletDetail);
        }

        public void UpdateWalletDetail(WalletDetail walletDetail)
        {
            if (walletDetail == null)
                throw new ArgumentNullException(nameof(walletDetail));

            _walletRepository.Update(walletDetail);

            //event notification
            _eventPublisher.EntityUpdated(walletDetail);
        }

        #endregion
    }
}
