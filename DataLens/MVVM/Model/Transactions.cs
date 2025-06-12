using DataLens.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLens.MVVM.Model
{
    public class Transaction : ObservableObject
    {
        private string _name;
        public DateTime? _date { get; set; }
        private string _category;
        private decimal _amount;

        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(); }
        }

        public DateTime? Date
        {
            get => _date;
            set { _date = value; OnPropertyChanged(); }
        }

        public string Category
        {
            get => _category;
            set { _category = value; OnPropertyChanged(); }
        }

        public decimal Amount
        {
            get => _amount;
            set { _amount = value; OnPropertyChanged(); }
        }
    }
}
