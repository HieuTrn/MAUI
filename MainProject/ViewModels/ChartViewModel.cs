using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System;
using MainProject.Database;
using MainProject.Models;
using Microsoft.Maui.Storage;
using Microsoft.Maui.Graphics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace MainProject.ViewModels;

public partial class ChartViewModel : MainViewModel
{
    private KetnoiDB _db;

    // 🔥 GỌN LẠI TOÀN BỘ PROPERTY
    [ObservableProperty] private bool isTheoThang = true;
    [ObservableProperty] private ObservableCollection<ISeries> bieudotron;
    [ObservableProperty] private ObservableCollection<ISeries> bieudocot;
    [ObservableProperty] private Axis[] x;
    [ObservableProperty] private Axis[] y;
    [ObservableProperty] private double tongthu;
    [ObservableProperty] private double tongchi;
    [ObservableProperty] private ObservableCollection<GiaoDichDisplay> danhSachGiaoDich;

    // 🔥 computed property giữ nguyên
    public Color MauNhanThang => IsTheoThang ? Color.FromArgb("#38D5BE") : Colors.White;
    public Color MauChuThang => IsTheoThang ? Colors.White : Color.FromArgb("#64748B");
    public Color MauNhanNam => !IsTheoThang ? Color.FromArgb("#38D5BE") : Colors.White;
    public Color MauChuNam => !IsTheoThang ? Colors.White : Color.FromArgb("#64748B");

    public ChartViewModel()
    {
        _db = new KetnoiDB();
        Y = new Axis[]
        {
            new Axis
            {
                LabelsPaint = new SolidColorPaint(SKColors.DimGray),
                TextSize = 14,
                MinLimit = 0
            }
        };
        LoadData();
    }

    // 🔥 Command giữ nguyên
    [RelayCommand]
    private void ChonTheoThang()
    {
        IsTheoThang = true;
        CapNhatUI();
        LoadData();
    }

    [RelayCommand]
    private void ChonTheoNam()
    {
        IsTheoThang = false;
        CapNhatUI();
        LoadData();
    }

    private void CapNhatUI()
    {
        OnPropertyChanged(nameof(MauNhanThang));
        OnPropertyChanged(nameof(MauChuThang));
        OnPropertyChanged(nameof(MauNhanNam));
        OnPropertyChanged(nameof(MauChuNam));
    }

    public void LoadData()
    {
        int userId = Preferences.Default.Get("UserID", 1);
        string username = Preferences.Default.Get("ngdunght", "");
        DateTime now = DateTime.Now;

        var allData = _db.LayDuLieuBieuDo(userId);
        var allGiaoDich = _db.hthigd(username);

        var filteredData = new List<KetnoiDB.ChartDataRow>();
        var filteredGiaoDich = new ObservableCollection<GiaoDichDisplay>();
        double thu = 0, chi = 0;

        foreach (var gd in allGiaoDich)
        {
            if (DateTime.TryParse(gd.Ngay, out DateTime ngayGd))
            {
                bool hopLe = IsTheoThang
                    ? (ngayGd.Month == now.Month && ngayGd.Year == now.Year)
                    : (ngayGd.Year == now.Year);

                if (hopLe)
                {
                    filteredGiaoDich.Add(gd);
                    if (gd.LoaiGD == "Thu Nhập") thu += gd.SoTien;
                    else if (gd.LoaiGD == "Chi Tiêu") chi += gd.SoTien;
                }
            }
        }

        foreach (var item in allData)
        {
            bool hopLe = IsTheoThang
                ? (item.Ngay.Month == now.Month && item.Ngay.Year == now.Year)
                : (item.Ngay.Year == now.Year);

            if (hopLe) filteredData.Add(item);
        }

        Tongthu = thu;
        Tongchi = chi;
        DanhSachGiaoDich = filteredGiaoDich;

        X = new Axis[]
        {
            new Axis
            {
                Labels = IsTheoThang
                    ? new string[] { "Tuần 1", "Tuần 2", "Tuần 3", "Tuần 4" }
                    : new string[] { "T1","T2","T3","T4","T5","T6","T7","T8","T9","T10","T11","T12" },
                LabelsPaint = new SolidColorPaint(SKColors.DimGray),
                TextSize = 14
            }
        };

        var dictPie = new Dictionary<string, double>();
        var dictCol = new Dictionary<string, double[]>();
        int soCotX = IsTheoThang ? 4 : 12;

        foreach (var item in filteredData)
        {
            if (!dictPie.ContainsKey(item.TenDanhMuc)) dictPie[item.TenDanhMuc] = 0;
            dictPie[item.TenDanhMuc] += item.SoTien;

            if (!dictCol.ContainsKey(item.TenDanhMuc)) dictCol[item.TenDanhMuc] = new double[soCotX];

            int indexCot = IsTheoThang
                ? Math.Min((item.Ngay.Day - 1) / 7, 3)
                : item.Ngay.Month - 1;

            dictCol[item.TenDanhMuc][indexCot] += item.SoTien;
        }

        SKColor[] mangMau =
        {
            SKColors.LimeGreen, SKColors.Orange, SKColors.RoyalBlue,
            SKColors.MediumPurple, SKColors.Tomato, SKColors.DeepPink
        };

        var newPieSeries = new ObservableCollection<ISeries>();
        var newColSeries = new ObservableCollection<ISeries>();
        int colorIndex = 0;

        foreach (var kvp in dictPie)
        {
            var color = mangMau[colorIndex % mangMau.Length];

            newPieSeries.Add(new PieSeries<double>
            {
                Values = new double[] { kvp.Value },
                Name = kvp.Key,
                Fill = new SolidColorPaint(color),
                InnerRadius = 40
            });

            newColSeries.Add(new ColumnSeries<double>
            {
                Values = dictCol[kvp.Key],
                Name = kvp.Key,
                Fill = new SolidColorPaint(color),
                MaxBarWidth = 15,
                Rx = 4,
                Ry = 4
            });

            colorIndex++;
        }

        Bieudotron = newPieSeries;
        Bieudocot = newColSeries;
    }
}