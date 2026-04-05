

using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using System.Collections.ObjectModel;

namespace MainProject.ViewModels
{
    public class ChartViewModel

    {
        public ObservableCollection<ISeries> Bieudo { get; set; }

        public ChartViewModel()
        {
            Bieudo = new ObservableCollection<ISeries>
            {
                new PieSeries<double>
                {
                    Values = new double[] { 40 },
                    Name = "Ăn uống",
                    Fill = new SolidColorPaint(SKColors.LimeGreen), 
                    InnerRadius = 40
                },
               
                new PieSeries<double>
                {
                    Values = new double[] { 30 },
                    Name = "Mua sắm",
                    Fill = new SolidColorPaint(SKColors.Orange), 
                    InnerRadius = 40
                },
                new PieSeries<double>
                {
                    Values = new double[] { 20 },
                    Name = "Hóa đơn",
                    Fill = new SolidColorPaint(SKColors.RoyalBlue), 
                    InnerRadius = 40
                },
                
                new PieSeries<double>
                {
                    Values = new double[] { 10 },
                    Name = "Giải trí",
                    Fill = new SolidColorPaint(SKColors.MediumPurple),
                    InnerRadius = 40 //
                }
            };

        }

    }
}
