using DataLens.Components;
using DataLens.Core;
using DataLens.MVVM.Model;
using DataLens.MVVM.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace DataLens.MVVM.ViewModel
{
    public class HomeViewModel : ObservableObject
    {
        public ObservableCollection<Transaction> Transactions { get; set; }
        public ICommand EditExpenseCommand { get; }
        public ICommand DeleteExpenseCommand { get; }
        public ICommand SortCommand { get; }

        private string _lastSortProperty; // last used for sorting
        private bool _sortDescending; // descending(true) / ascending(false)

        // Constructor
        public HomeViewModel()
        {
            Transactions = new ObservableCollection<Transaction>(TransactionStorage.Load());
            EditExpenseCommand = new RelayCommand(EditExpense);
            DeleteExpenseCommand = new RelayCommand(DeleteExpense);
            SortCommand = new RelayCommand(Sort);
        }

        private void EditExpense(object parameter)
        {
            if (parameter is Transaction selectedTransaction)
            {
                var editWindow = new EditWindow();

                // Save or Cancel Implementation
                var transactionCopy = new Transaction
                {
                    Name = selectedTransaction.Name,
                    Date = selectedTransaction.Date,
                    Category = selectedTransaction.Category,
                    Amount = selectedTransaction.Amount
                };
                editWindow.DataContext = transactionCopy;

                if (editWindow.ShowDialog() == true)
                {
                    selectedTransaction.Name = transactionCopy.Name;
                    selectedTransaction.Date = transactionCopy.Date;
                    selectedTransaction.Category = transactionCopy.Category;
                    selectedTransaction.Amount = transactionCopy.Amount;
                    TransactionStorage.Save(Transactions);
                }
            }
        }

        private void DeleteExpense(object parameter)
        {
            if (parameter is Transaction selectedTransaction)
            {
                Transactions.Remove(selectedTransaction);
                TransactionStorage.Save(Transactions);
            }
        }

        private void Sort(object parameter)
        {
            string property = parameter as string; // If string remains string else becomes null
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
                    sorted = _sortDescending
                        ? Transactions.OrderByDescending(t => t.Name)
                        : Transactions.OrderBy(t => t.Name);
                    break;
                case "Date":
                    sorted = _sortDescending
                        ? Transactions.OrderByDescending(t => t.Date)
                        : Transactions.OrderBy(t => t.Date);
                    break;
                case "Category":
                    sorted = _sortDescending
                        ? Transactions.OrderByDescending(t => t.Category)
                        : Transactions.OrderBy(t => t.Category);
                    break;
                case "Amount":
                    sorted = _sortDescending
                        ? Transactions.OrderByDescending(t => t.Amount)
                        : Transactions.OrderBy(t => t.Amount);
                    break;
                default:
                    sorted = Transactions;
                    break;
            }

            Transactions = new ObservableCollection<Transaction>(sorted);
            OnPropertyChanged(nameof(Transactions));
        }


        public void AddTransaction(Transaction tx)
        {
            Transactions.Add(tx);
            TransactionStorage.Save(Transactions);
        }
    }
}