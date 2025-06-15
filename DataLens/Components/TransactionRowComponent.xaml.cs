using DataLens.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DataLens.Components
{
    /// <summary>
    /// Interaction logic for TransactionRowComponent.xaml
    /// </summary>
    public partial class TransactionRowComponent : UserControl
    {
        public TransactionRowComponent()
        {
            InitializeComponent();
        }

        private void EditExpense(object sender, RoutedEventArgs e)
        {
            var transaction = DataContext as Transaction;
        }

        private void DeleteExpense(object sender, RoutedEventArgs e)
        {
            var transaction = DataContext as Transaction;
        }
    }

}
