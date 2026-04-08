using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MainProject.Database;
using MainProject.Models;
using MainProject.Pages;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;

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
        try
        {
            DanhSachGiaoDich = App.ketnoiDB.hthigd(Users);

        }
        catch (Exception ex)
        {
            Debug.WriteLine("❌ Lỗi: " + ex.ToString());
        } 
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

            
            if (gd.LoaiGD == "Thu Nhập")
            {

                thu += gd.SoTien;
            } else if(gd.LoaiGD == "Chi Tiêu")
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
    

[RelayCommand]
    private async Task InBaoCao()
    {
        if (DanhSachGiaoDich == null || DanhSachGiaoDich.Count == 0)
        {
            await Application.Current.MainPage.DisplayAlert("Thông báo", "Không có dữ liệu giao dịch để in báo cáo!", "OK");
            return;
        }

        try
        {
            var csv = new StringBuilder();

            csv.AppendLine("Ngày,Loại Giao Dịch,Hạng Mục,Số Tiền (VND),Ghi Chú");

            foreach (var gd in DanhSachGiaoDich)
            {
                string ghiChuAnToan = gd.GhiChu?.Replace(",", " ") ?? "";

                csv.AppendLine($"{gd.Ngay},{gd.LoaiGD},{gd.TenDanhMuc},{gd.SoTien},{ghiChuAnToan}");
            }

            string tenFile = $"BaoCao_HoangPhanMoney_{DateTime.Now:dd_MM_yyyy}.csv";

            string duongDanFile = Path.Combine(FileSystem.CacheDirectory, tenFile);

            File.WriteAllText(duongDanFile, csv.ToString(), new UTF8Encoding(true));

            await Share.Default.RequestAsync(new ShareFileRequest
            {
                Title = "Lưu hoặc chia sẻ Báo Cáo",
                File = new ShareFile(duongDanFile)
            });
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Lỗi", $"Không thể tạo file báo cáo:\n{ex.Message}", "OK");
        }
    }




}