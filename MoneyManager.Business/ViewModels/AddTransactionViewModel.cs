﻿using Microsoft.Practices.ServiceLocation;
using MoneyManager.DataAccess.DataAccess;
using MoneyManager.DataAccess.Model;
using MoneyManager.Foundation;
using MoneyManager.Src;
using System;
using System.Collections.ObjectModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace MoneyManager.ViewModels
{
    internal class AddTransactionViewModel
    {
        public FinancialTransaction SelectedTransaction
        {
            get { return ServiceLocator.Current.GetInstance<TransactionDataAccess>().SelectedTransaction; }
            set { ServiceLocator.Current.GetInstance<TransactionDataAccess>().SelectedTransaction = value; }
        }

        public ObservableCollection<Account> AllAccounts
        {
            get { return ServiceLocator.Current.GetInstance<AccountDataAccess>().AllAccounts; }
        }

        public ObservableCollection<Category> AllCategories
        {
            get { return ServiceLocator.Current.GetInstance<CategoryDataAccess>().AllCategories; }
        }

        public string Title
        {
            get
            {
                string text = IsEdit
                    ? Translation.GetTranslation("EditTitle")
                    : Translation.GetTranslation("AddTitle");

                string type = TransactionTypeHelper.GetViewTitleForType(SelectedTransaction.Type);

                return String.Format(text, type);
            }
        }

        public DateTime EndDate { get; set; }

        public bool IsEndless { get; set; }

        public bool IsEdit { get; set; }

        public int Recurrence { get; set; }

        public bool IsTransfer { get; set; }

        public void Save()
        {
            if (IsEdit)
            {
                ServiceLocator.Current.GetInstance<TransactionDataAccess>().Update(SelectedTransaction);
            }
            else
            {
                ServiceLocator.Current.GetInstance<TransactionDataAccess>().Save(SelectedTransaction);
            }

            ((Frame) Window.Current.Content).GoBack();
        }

        public void Cancel()
        {
            //TODO: refactor
            //if (IsEdit)
            //{
            //    ServiceLocator.Current.GetInstance<AccountDataAccess>().AddTransactionAmount(SelectedTransaction);
            //}

            ((Frame) Window.Current.Content).GoBack();
        }
    }
}