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





    [RelayCommand]
    async void GotoDanhMuc()
    {
        await Shell.Current.GoToAsync(nameof(DanhMucPage));
    }

    public MainViewModel()
    {
        Users = Preferences.Default.Get("ngdunght", "");
        LoadDuLieu();
        Tinhtong();

    }
    public void LoadDuLieu()
    {
        DanhSachGiaoDich = new ObservableCollection<GiaoDichDisplay>();
        DanhSachGiaoDich = App.ketnoiDB.hthigd(Users);
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