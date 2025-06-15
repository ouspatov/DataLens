using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using DataLens.Components;
using DataLens.Core;
using DataLens.MVVM.Model;
using DataLens.MVVM.Services;


namespace DataLens.MVVM.ViewModel
{
    public class HomeViewModel : ObservableObject
    {
        public ObservableCollection<Transaction> Transactions { get; set; }
        public ICommand EditExpenseCommand { get; }
        public ICommand DeleteExpenseCommand { get; }
        public ICommand SortCommand { get; }

        private string _lastSortProperty;
        private bool _sortDescending;

        public HomeViewModel()
        {
            Transactions = new ObservableCollection<Transaction>(TransactionStorage.Load());
            EditExpenseCommand = new RelayCommand(EditExpense);
            DeleteExpenseCommand = new RelayCommand(DeleteExpense);
            SortCommand = new RelayCommand(Sort);
        }

        private void EditExpense(object parameter)
        {
            if (parameter is Transaction selected)
            {
                var editWindow = new EditWindow();

                var transactionCopy = new Transaction
                {
                    Name = selected.Name,
                    Date = selected.Date,
                    Category = selected.Category,
                    Amount = selected.Amount
                };

                editWindow.DataContext = transactionCopy;

                if (editWindow.ShowDialog() == true)
                {
                    selected.Name = transactionCopy.Name;
                    selected.Date = transactionCopy.Date;
                    selected.Category = transactionCopy.Category;
                    selected.Amount = transactionCopy.Amount;

                    LogHistory("Edit", selected);
                }
            }
        }

        private void DeleteExpense(object parameter)
        {
            if (parameter is Transaction selectedTransaction)
            {
                Transactions.Remove(selectedTransaction);
                LogHistory("Delete", selectedTransaction);
            }
        }

        public void AddTransaction(Transaction tx)
        {
            Transactions.Add(tx);
            LogHistory("Add", tx);
        }

        private void Sort(object parameter)
        {
            string property = parameter as string;
            if (string.IsNullOrEmpty(property)) return;

            if (_lastSortProperty == property)
            {
                _sortDescending = !_sortDescending;
            }
            else
            {
                _sortDescending = false;
                _lastSortProperty = property;
            }

            IEnumerable<Transaction> sorted;
            switch (property)
            {
                case "Name":
                    sorted = _sortDescending ? Transactions.OrderByDescending(t => t.Name) : Transactions.OrderBy(t => t.Name);
                    break;
                case "Date":
                    sorted = _sortDescending ? Transactions.OrderByDescending(t => t.Date) : Transactions.OrderBy(t => t.Date);
                    break;
                case "Category":
                    sorted = _sortDescending ? Transactions.OrderByDescending(t => t.Category) : Transactions.OrderBy(t => t.Category);
                    break;
                case "Amount":
                    sorted = _sortDescending ? Transactions.OrderByDescending(t => t.Amount) : Transactions.OrderBy(t => t.Amount);
                    break;
                default:
                    sorted = Transactions;
                    break;
            }

            Transactions = new ObservableCollection<Transaction>(sorted);
            OnPropertyChanged(nameof(Transactions));
        }

        private void LogHistory(string action, Transaction transaction)
        {
            TransactionStorage.Save(Transactions);
            HistoryStorage.AddRecord(new HistoryRecord
            {
                Timestamp = DateTime.Now,
                Action = action,
                ItemName = transaction.Name,
                Details = $"Date: {transaction.Date:yyyy-MM-dd}, Category: {transaction.Category}, Amount: {transaction.Amount}"
            });
        }
    }
}
