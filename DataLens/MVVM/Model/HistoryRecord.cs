using DataLens.Core;
using System;

namespace DataLens.MVVM.Model
{
    public class HistoryRecord : ObservableObject
    {
        public DateTime Timestamp { get; set; }
        public string Action { get; set; }
        public string ItemName { get; set; }
        public string Details { get; set; }
    }
}
