using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
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
    String _text1;

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
        if (!string.IsNullOrWhiteSpace(Text))
        {
            danhmuc newDanhMuc = new danhmuc
            {
                ten = Text,
                LoaiGD = Text1
            };
            _dB.ThemDanhmuc(Text, Text1);
            LoadData();
            Text = string.Empty;
            Text1 = string.Empty;
        }
    }
}