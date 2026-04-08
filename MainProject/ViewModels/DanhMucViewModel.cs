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

    [ObservableProperty]
    danhmuc _danhMucDuocChon;

    [RelayCommand]
    private void XoaDanhMucDangChon()
    {
        if (DanhMucDuocChon == null)
        {
            App.Current.MainPage.DisplayAlert("Thông báo", "Vui lòng chọn một danh mục để xóa!", "OK");
            return;
        }

        bool xoaThanhCong = _dB.XoaDanhmuc(DanhMucDuocChon.Id);
        if (xoaThanhCong)
        {
            DanhmucList.Remove(DanhMucDuocChon);
            DanhMucDuocChon = null; // Xóa xong thì reset trạng thái chọn
        }
        else
        {
            App.Current.MainPage.DisplayAlert("Lỗi", "Không thể xóa vì đã có giao dịch!", "Đóng");
        }
    }

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