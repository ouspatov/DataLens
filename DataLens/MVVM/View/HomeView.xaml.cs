using DataLens.Components;
using DataLens.MVVM.ViewModel;
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

namespace DataLens.MVVM.View
{
    public partial class HomeView : UserControl
    {
        public HomeView()
        {
            InitializeComponent();
            DataContext = new HomeViewModel();
        }
        private void AddExpenseClick(object sender, RoutedEventArgs e)
        {
            var window = new EditWindow();
            if (window.ShowDialog() == true)
            {
                var viewModel = DataContext as HomeViewModel;
                viewModel?.AddTransaction(window.ResultTransaction);
            }
        }
    }
}
