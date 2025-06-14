using DataLens.Core;
using System;

namespace DataLens.MVVM.ViewModel
{
    internal class MainViewModel : ObservableObject
    {
        private object _currentView;
        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand LensViewCommnand { get; set; }
        public RelayCommand HistoryViewCommand { get; set; }
        public RelayCommand QuitCommand { get; set; }

        public HomeViewModel HomeVM { get; set; }
        public LensViewModel LensVM { get; set; }
        public HistoryViewModel HistoryVM { get; set; }

        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            HomeVM = new HomeViewModel();
            LensVM = new LensViewModel();
            HistoryVM = new HistoryViewModel();

            CurrentView = HomeVM;

            HomeViewCommand = new RelayCommand(o =>
            {
                CurrentView = HomeVM;
            });

            LensViewCommnand = new RelayCommand(o =>
            {
                CurrentView = LensVM;
            });

            HistoryViewCommand = new RelayCommand(o =>
            {
                CurrentView = HistoryVM;
            });

            QuitCommand = new RelayCommand(o => System.Windows.Application.Current.Shutdown());
        }
    }
}
