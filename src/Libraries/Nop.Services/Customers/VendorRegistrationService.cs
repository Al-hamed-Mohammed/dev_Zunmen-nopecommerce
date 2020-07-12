using System;
using System.Linq;
using Nop.Core;
using Nop.Core.Domain.Customers;
using Nop.Services.Common;
using Nop.Services.Events;
using Nop.Services.Localization;
using Nop.Services.Messages;
using Nop.Services.Orders;
using Nop.Services.Security;
using Nop.Services.Stores;

namespace Nop.Services.Customers
{
    /// <summary>
    /// Customer registration service
    /// </summary>
    public partial class VendorRegistrationService : IVendorRegistrationService
    {
        #region Fields

        private readonly CustomerSettings _customerSettings;
        private readonly ICustomerService _customerService;
        private readonly IEncryptionService _encryptionService;
        private readonly IEventPublisher _eventPublisher;
        private readonly ILocalizationService _localizationService;
        private readonly IRewardPointService _rewardPointService;
        private readonly RewardPointsSettings _rewardPointsSettings;

        #endregion

        #region Ctor

        public VendorRegistrationService(CustomerSettings customerSettings,
            ICustomerService customerService,
            IEncryptionService encryptionService,
            IEventPublisher eventPublisher,
            ILocalizationService localizationService,
            IRewardPointService rewardPointService,
            RewardPointsSettings rewardPointsSettings)
        {
            _customerSettings = customerSettings;
            _customerService = customerService;
            _encryptionService = encryptionService;
            _eventPublisher = eventPublisher;
            _localizationService = localizationService;
            _rewardPointService = rewardPointService;
            _rewardPointsSettings = rewardPointsSettings;
        }

        #endregion


        #region Methods

       
        /// <summary>
        /// Register customer
        /// </summary>
        /// <param name="request">Request</param>
        /// <returns>Result</returns>
        public virtual CustomerRegistrationResult RegisterCustomer(CustomerRegistrationRequest request, string returnUrl)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (request.Customer == null)
                throw new ArgumentException("Can't load current customer");

            var result = new CustomerRegistrationResult();
            if (request.Customer.IsSearchEngineAccount())
            {
                result.AddError("Search engine can't be registered");
                return result;
            }

            if (request.Customer.IsBackgroundTaskAccount())
            {
                result.AddError("Background task account can't be registered");
                return result;
            }

            if (request.Customer.IsRegistered())
            {
                result.AddError("Current customer is already registered");
                return result;
            }

            if (string.IsNullOrEmpty(request.Email))
            {
                result.AddError(_localizationService.GetResource("Account.Register.Errors.EmailIsNotProvided"));
                return result;
            }

            if (!CommonHelper.IsValidEmail(request.Email))
            {
                result.AddError(_localizationService.GetResource("Common.WrongEmail"));
                return result;
            }

            if (string.IsNullOrWhiteSpace(request.Password))
            {
                result.AddError(_localizationService.GetResource("Account.Register.Errors.PasswordIsNotProvided"));
                return result;
            }

            if (_customerSettings.UsernamesEnabled && string.IsNullOrEmpty(request.Username))
            {
                result.AddError(_localizationService.GetResource("Account.Register.Errors.UsernameIsNotProvided"));
                return result;
            }

            //validate unique user
            if (_customerService.GetCustomerByEmail(request.Email) != null)
            {
                result.AddError(_localizationService.GetResource("Account.Register.Errors.EmailAlreadyExists"));
                return result;
            }

            if (_customerSettings.UsernamesEnabled && _customerService.GetCustomerByUsername(request.Username) != null)
            {
                result.AddError(_localizationService.GetResource("Account.Register.Errors.UsernameAlreadyExists"));
                return result;
            }

            //at this point request is valid
            request.Customer.Username = request.Username;
            request.Customer.Email = request.Email;

            var customerPassword = new CustomerPassword
            {
                Customer = request.Customer,
                PasswordFormat = request.PasswordFormat,
                CreatedOnUtc = DateTime.UtcNow
            };
            switch (request.PasswordFormat)
            {
                case PasswordFormat.Clear:
                    customerPassword.Password = request.Password;
                    break;
                case PasswordFormat.Encrypted:
                    customerPassword.Password = _encryptionService.EncryptText(request.Password);
                    break;
                case PasswordFormat.Hashed:
                    var saltKey = _encryptionService.CreateSaltKey(NopCustomerServiceDefaults.PasswordSaltKeySize);
                    customerPassword.PasswordSalt = saltKey;
                    customerPassword.Password = _encryptionService.CreatePasswordHash(request.Password, saltKey, _customerSettings.HashedPasswordFormat);
                    break;
            }

            _customerService.InsertCustomerPassword(customerPassword);

            request.Customer.Active = request.IsApproved;

            //add to 'Registered' role
            var registeredRole = _customerService.GetCustomerRoleBySystemName(NopCustomerDefaults.RegisteredRoleName);
            if (registeredRole == null)
                throw new NopException("'Registered' role could not be loaded");
            //request.Customer.CustomerRoles.Add(registeredRole);
            request.Customer.AddCustomerRoleMapping(new CustomerCustomerRoleMapping { CustomerRole = registeredRole });

            //add to 'Vendor' role
            var vendorRole = _customerService.GetCustomerRoleBySystemName(NopCustomerDefaults.VendorsRoleName);
            if (vendorRole == null)
                throw new NopException("'Vendor' role could not be loaded");
            //request.Customer.CustomerRoles.Add(vendorRole);
            request.Customer.AddCustomerRoleMapping(new CustomerCustomerRoleMapping { CustomerRole = vendorRole });

            //remove from 'Guests' role
            var guestRole = request.Customer.CustomerRoles.FirstOrDefault(cr => cr.SystemName == NopCustomerDefaults.GuestsRoleName);
            if (guestRole != null)
            {
                //request.Customer.CustomerRoles.Remove(guestRole);
                request.Customer.RemoveCustomerRoleMapping(
                    request.Customer.CustomerCustomerRoleMappings.FirstOrDefault(mapping => mapping.CustomerRoleId == guestRole.Id));
            }

            //add reward points for customer registration (if enabled)
            if (_rewardPointsSettings.Enabled && _rewardPointsSettings.PointsForRegistration > 0)
            {
                var endDate = _rewardPointsSettings.RegistrationPointsValidity > 0
                    ? (DateTime?)DateTime.UtcNow.AddDays(_rewardPointsSettings.RegistrationPointsValidity.Value) : null;
                _rewardPointService.AddRewardPointsHistoryEntry(request.Customer, _rewardPointsSettings.PointsForRegistration,
                    request.StoreId, _localizationService.GetResource("RewardPoints.Message.EarnedForRegistration"), endDate: endDate);
            }

            _customerService.UpdateCustomer(request.Customer);

            return result;
        }

        #endregion
    }
}