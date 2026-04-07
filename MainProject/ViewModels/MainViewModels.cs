using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MainProject.Pages;
using MainProject.Models;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace MainProject.ViewModels;
    public partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    private string _users;
    [ObservableProperty]
    private ObservableCollection<GiaoDichDisplay> _danhSachGiaoDich;
    [ObservableProperty]
    private double _tongthu;
    [ObservableProperty]
    private double _tongchi;
    [ObservableProperty]
    private double _tongSoDu;





    public MainViewModel()
    {
        Users = Preferences.Default.Get("ngdunght", "");
        LoadDuLieu();
        Tinhtong();

    }
    public void LoadDuLieu()
    {
        DanhSachGiaoDich = new ObservableCollection<GiaoDichDisplay>();
        DanhSachGiaoDich.Add(new GiaoDichDisplay
        {
            Id = 1,
            TenDanhMuc = "Ăn uống",
            GhiChu = "Ăn sáng bún bò huế",
            SoTien = 45000,
            LoaiGD = "Chi",
            Ngay = "07/04/2026"
        });

        DanhSachGiaoDich.Add(new GiaoDichDisplay
        {
            Id = 2,
            TenDanhMuc = "Tiền lương",
            GhiChu = "Lương tháng 3",
            SoTien = 1500088000,
            LoaiGD = "Thu",
            Ngay = "05/04/2026"
        });

        DanhSachGiaoDich.Add(new GiaoDichDisplay
        {
            Id =4,
            TenDanhMuc = "Hóa đơn",
            GhiChu = "Tiền điện",
            SoTien = 85007700,
            LoaiGD = "Chi",
            Ngay = "01/04/2026"
        });
        DanhSachGiaoDich.Add(new GiaoDichDisplay
        {
            Id = 5,
            TenDanhMuc = "Hóa đơn",
            GhiChu = "Tiền điện",
            SoTien = 85660000,
            LoaiGD = "Chi",
            Ngay = "01/04/2026"
        });
        DanhSachGiaoDich.Add(new GiaoDichDisplay
        {
            Id = 6,
            TenDanhMuc = "Hóa đơn",
            GhiChu = "Tiền yyyy",
            SoTien = 666666,
            LoaiGD = "Chi",
            Ngay = "01/04/2026"
        });
        DanhSachGiaoDich.Add(new GiaoDichDisplay
        {
            Id = 7,
            TenDanhMuc = "Hóa đơn",
            GhiChu = "Tiền ffff",
            SoTien = 123446,
            LoaiGD = "Chi",
            Ngay = "01/04/2026"
        });
        DanhSachGiaoDich.Add(new GiaoDichDisplay
        {
            Id = 8,
            TenDanhMuc = "Hóa ssss ",
            GhiChu = "đwdwd",
            SoTien = 122345,
            LoaiGD = "Chi",
            Ngay = "01/04/2026"
        });

        //DanhSachGiaoDich = App.ketnoiDB.hthidg();
    }
    public void Tinhtong()
    {
        if (DanhSachGiaoDich == null || DanhSachGiaoDich.Count == 0) {
            TongSoDu = 0;
            Tongthu = 0;
            Tongchi = 0;
            return;
        }
        double thu = 0;
        double chi = 0;

       
        for (int i = 0; i < DanhSachGiaoDich.Count; i++)
        {
            
            var gd = DanhSachGiaoDich[i];

            
            if (gd.LoaiGD == "Thu")
            {

                thu += gd.SoTien;
            } else if(gd.LoaiGD == "Chi")
            {
                chi += gd.SoTien;
            }
            Tongthu = thu;
            Tongchi = chi;
            TongSoDu = Tongthu - Tongchi;
        }
    }

    [RelayCommand]
    async Task NavigateToDanhMuc()
    {
        await Shell.Current.GoToAsync(nameof(DanhMucPage));
    }
    [RelayCommand]
    async Task ThemGiaoDich()
    {
        await Shell.Current.GoToAsync(nameof(ThemGiaoDichPage));
    }



}