using DataLens.Core;
using DataLens.MVVM.Model;
using DataLens.MVVM.Services;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataLens.MVVM.ViewModel
{
    public class LensViewModel : ObservableObject
    {
        public ISeries[] PieSeries { get; set; }
        public ISeries[] LineSeries { get; set; }
        public Axis[] LineXAxes { get; set; }
        public Axis[] LineYAxes { get; set; }

        public LensViewModel()
        {
            var transactions = TransactionStorage.Load();

            SKColor[]Palette = new SKColor[]
            {
                new SKColor(227, 151, 179),
                new SKColor(43, 182, 168),
                new SKColor(229, 150, 75),
                new SKColor(161, 128, 229),
                new SKColor(73, 121, 231),
            };

            var months = transactions
                .Where(t => t.Date != null)
                .Select(t => new DateTime(t.Date.Value.Year, t.Date.Value.Month, 1))
                .Distinct()
                .OrderBy(d => d)
                .ToList();

            var categories = transactions
                .Select(t => t.Category)
                .Distinct()
                .ToList();

            PieSeries = categories.Select((category, i) => new PieSeries<double>
            {
                Name = category,
                Values = new[] { (double)transactions.Where(t => t.Category == category).Sum(x => x.Amount) },
                Fill = new SolidColorPaint(Palette[i % Palette.Length]),
                DataLabelsSize = 16,
            }).ToArray();

            var lineSeriesList = new List<LineSeries<double>>();
            for (int i = 0; i < categories.Count; i++)
            {
                var category = categories[i];
                var values = months.Select(month =>
                    transactions
                        .Where(t => t.Category == category &&
                                    t.Date != null &&
                                    new DateTime(t.Date.Value.Year, t.Date.Value.Month, 1) == month)
                        .Sum(t => (double)t.Amount)
                ).ToArray();

                lineSeriesList.Add(new LineSeries<double>
                {
                    Name = category,
                    Values = values,
                    Stroke = new SolidColorPaint(Palette[i % Palette.Length], 4),
                    GeometrySize = 12,
                    LineSmoothness = 0,
                    GeometryFill = new SolidColorPaint(Palette[i % Palette.Length]),
                });
            }

            LineSeries = lineSeriesList.ToArray();

            LineXAxes = new Axis[]
            {
                new Axis
                {
                    Labels = months.Select(m => m.ToString("yyyy-MM")).ToArray(),
                    Name = "Month",
                    LabelsPaint = new SolidColorPaint(SKColors.White),
                    NamePaint = new SolidColorPaint(SKColors.White),
                    TextSize = 16,
                }
            };

            LineYAxes = new Axis[]
            {
                new Axis
                {
                    Name = "Expenditure",
                    LabelsPaint = new SolidColorPaint(SKColors.White),
                    NamePaint = new SolidColorPaint(SKColors.White),
                    TextSize = 16,
                }
            };
        }
    }
}
