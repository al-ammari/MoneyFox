﻿using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using MoneyManager.Foundation.Model;
using MoneyManager.Foundation.OperationContracts;

namespace MoneyManager.Business.ViewModels
{
    public class AccountListUserControlViewModel : ViewModelBase
    {
        private readonly IRepository<Account> _accountRepository;

        public AccountListUserControlViewModel(IRepository<Account> accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public ObservableCollection<Account> AllAccounts
        {
            get { return _accountRepository.Data; }
            set { _accountRepository.Data = value; }
        }

        public void Delete(Account item)
        {
            _accountRepository.Delete(item);
        }
    }
}