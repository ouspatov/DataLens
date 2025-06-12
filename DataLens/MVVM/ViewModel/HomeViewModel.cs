using DataLens.Components;
using DataLens.Core;
using DataLens.MVVM.Model;
using DataLens.MVVM.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DataLens.MVVM.ViewModel
{
    public class HomeViewModel : ObservableObject
    {
        public ObservableCollection<Transaction> Transactions { get; set; }
        public ICommand EditExpenseCommand { get; }
        public ICommand DeleteExpenseCommand { get; }
        public HomeViewModel()
        {
            Transactions = new ObservableCollection<Transaction>(TransactionStorage.Load());
            EditExpenseCommand = new RelayCommand(EditExpense);
            DeleteExpenseCommand = new RelayCommand(DeleteExpense);
        }

        private void EditExpense(object parameter)
        {
            if (parameter is Transaction selectedTransaction)
            {
                var editWindow = new EditWindow();
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

        public void AddTransaction(Transaction tx)
        {
            Transactions.Add(tx);
            TransactionStorage.Save(Transactions);
        }
    }
}