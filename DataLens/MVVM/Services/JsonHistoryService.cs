using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using DataLens.MVVM.Model;

namespace DataLens.MVVM.Services
{
    public static class HistoryStorage
    {
        private static readonly string AppDataFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "DataLens");
        private static readonly string FilePath = Path.Combine(AppDataFolder, "history.json");

        public static List<HistoryRecord> Load()
        {
            if (!Directory.Exists(AppDataFolder))
                Directory.CreateDirectory(AppDataFolder);

            if (!File.Exists(FilePath))
            {
                File.WriteAllText(FilePath, "[]");
                return new List<HistoryRecord>();
            }

            string json = File.ReadAllText(FilePath);
            return JsonSerializer.Deserialize<List<HistoryRecord>>(json) ?? new List<HistoryRecord>();
        }

        public static void Save(IEnumerable<HistoryRecord> records)
        {
            if (!Directory.Exists(AppDataFolder))
                Directory.CreateDirectory(AppDataFolder);

            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(records, options);
            File.WriteAllText(FilePath, json);
        }

        public static void AddRecord(HistoryRecord record)
        {
            var records = Load();
            records.Add(record);
            Save(records);
        }
    }
}
