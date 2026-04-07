using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using MainProject.Database;
using MainProject.Models;
using MainProject.Pages;

namespace MainProject.ViewModels;

public partial class DanhMucViewModel : ObservableObject
{
    private readonly KetnoiDB _dB = new KetnoiDB();

    [ObservableProperty]
    ObservableCollection<danhmuc> _danhmucList;

    [ObservableProperty]
    String _text;

    [ObservableProperty]
    List<string> _danhSachLoaiGD = new List<string> { "Thu Nhập", "Chi Tiêu" };

    [ObservableProperty]
    string _loaiGDDuocChon;

    public DanhMucViewModel()
    {
        LoadData();
    }

    private void LoadData()
    {
        ObservableCollection<danhmuc> data = _dB.layDanhmuc();
        DanhmucList = data;
    }

    [RelayCommand]
    private void XoaDanhMuc(danhmuc dm)
    {
        if (dm == null) return;
        _dB.XoaDanhmuc(dm.Id);
        DanhmucList.Remove(dm);
    }


    [RelayCommand]
    private void ThemDanhMuc()
    {
        if (!string.IsNullOrWhiteSpace(Text) && !string.IsNullOrWhiteSpace(LoaiGDDuocChon))
        {
            danhmuc newDanhMuc = new danhmuc
            {
                ten = Text
            };
            _dB.ThemDanhmuc(Text, LoaiGDDuocChon);
            LoadData();
            Text = string.Empty;
            LoaiGDDuocChon = null;
        }
    }
}