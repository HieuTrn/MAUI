using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using System.Collections.ObjectModel;

namespace MainProject.ViewModels
{
    public partial class ChartViewModel : MainViewModel

    {
        public ObservableCollection<ISeries> Bieudotron { get; set; }
        public ObservableCollection<ISeries> Bieudocot { get; set; }
        public Axis[] X { get; set; }
        public Axis[] Y { get; set; }
        public ChartViewModel()
        {
            Bieudotron = new ObservableCollection<ISeries>
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

            Bieudocot = new ObservableCollection<ISeries>
            {
                
                new ColumnSeries<double>
                {
                    Values = new double[] { 30, 0, 25, 0, 15, 0, 0 },
                    Name = "Ăn Uống ",
                    Fill = new SolidColorPaint(SKColors.LimeGreen),
                    MaxBarWidth = 15, // Chỉnh độ rộng cột cho thon gọn
                    Rx = 4, Ry = 4    // Bo tròn góc trên của cột
                },
               
                new ColumnSeries<double>
                {
                    Values = new double[] { 0, 25, 0, 0, 0, 0, 0 },
                    Name = "Mua Sắm  ",
                    Fill = new SolidColorPaint(SKColors.Orange),
                    MaxBarWidth = 15,
                    Rx = 4, Ry = 4
                },
                
                new ColumnSeries<double>
                {
                    Values = new double[] { 0, 30, 20, 25, 30, 20, 32 },
                    Name = "Khác  ",
                    Fill = new SolidColorPaint(SKColors.Tomato),
                    MaxBarWidth = 15,
                    Rx = 4, Ry = 4
                },
                
                new ColumnSeries<double>
                {
                    Values = new double[] { 0, 35, 30, 32, 40, 0, 0 },
                    Name = "Hóa Đơn  ",
                    Fill = new SolidColorPaint(SKColors.RoyalBlue),
                    MaxBarWidth = 15,
                    Rx = 4, Ry = 4
                },
                
                new ColumnSeries<double>
                {
                    Values = new double[] { 0, 0, 0, 0, 0, 0, 45 },
                    Name = "Giải Trí ",
                    Fill = new SolidColorPaint(SKColors.MediumPurple),
                    MaxBarWidth = 15,
                    Rx = 4, Ry = 4
                }
            };

            
            X = new Axis[]
            {
                new Axis
                {
                    Labels = new string[] { "T2", "T3", "T4", "T5", "T6", "T7", "CN" },
                    LabelsPaint = new SolidColorPaint(SKColors.DimGray),
                    TextSize = 14
                }
            };

            
            Y = new Axis[]
            {
                new Axis
                {
                    LabelsPaint = new SolidColorPaint(SKColors.DimGray),
                    TextSize = 14,
                    MinLimit = 0 
                }
            };
        }

    }
}