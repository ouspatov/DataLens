using DataLens.MVVM.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace DataLens.MVVM.Services
{
    public static class TransactionStorage
    {
        private static readonly string AppDataFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "DataLens");
        private static readonly string FilePath = Path.Combine(AppDataFolder, "transactions.json");

        public static List<Transaction> Load()
        {
            if (!Directory.Exists(AppDataFolder))
                Directory.CreateDirectory(AppDataFolder);

            if (!File.Exists(FilePath))
            {
                File.WriteAllText(FilePath, "[]");
                return new List<Transaction>();
            }

            string json = File.ReadAllText(FilePath);
            return JsonSerializer.Deserialize<List<Transaction>>(json) ?? new List<Transaction>();
        }

        public static void Save(IEnumerable<Transaction> transactions)
        {
            if (!Directory.Exists(AppDataFolder))
                Directory.CreateDirectory(AppDataFolder);

            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(transactions, options);
            File.WriteAllText(FilePath, json);
        }
    }
}
