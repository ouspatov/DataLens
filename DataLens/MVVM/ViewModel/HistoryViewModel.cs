using DataLens.Core;
using DataLens.MVVM.Model;
using DataLens.MVVM.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DataLens.MVVM.ViewModel
{
    public class HistoryViewModel : ObservableObject
    {
        public ObservableCollection<HistoryRecord> Records { get; set; }
        public ICommand ClearLogCommand { get; }

        public HistoryViewModel()
        {
            LoadHistory();
            ClearLogCommand = new RelayCommand(ClearLog);
        }

        public void LoadHistory()
        {
            var records = HistoryStorage.Load();
            Records = new ObservableCollection<HistoryRecord>(records.OrderByDescending(r => r.Timestamp));
            OnPropertyChanged(nameof(Records));
        }
        private void ClearLog(object parameter)
        {
            HistoryStorage.Save(new List<HistoryRecord>());
            Records.Clear();
        }
    }
}
