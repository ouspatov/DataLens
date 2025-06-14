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
using System.Windows.Shapes;

namespace DataLens.Components
{
    /// <summary>
    /// Interaction logic for EditWindow.xaml
    /// </summary>
    /// 
    public partial class EditWindow : Window
    {
        public Transaction ResultTransaction { get; private set; } = new Transaction();

        public EditWindow()
        {
            InitializeComponent();
            DataContext = ResultTransaction;
        }
        private void SaveButtonClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
