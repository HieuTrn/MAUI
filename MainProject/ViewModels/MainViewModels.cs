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
        // Kiểm tra xem danh sách có trống không
        if (DanhSachGiaoDich == null || DanhSachGiaoDich.Count == 0)
        {
            await Application.Current.MainPage.DisplayAlert("Thông báo", "Không có dữ liệu giao dịch để in báo cáo!", "OK");
            return;
        }

        try
        {
            // 1. Tạo nội dung file Excel (CSV)
            var csv = new StringBuilder();

            // Tạo dòng tiêu đề các cột
            csv.AppendLine("Ngày,Loại Giao Dịch,Hạng Mục,Số Tiền (VND),Ghi Chú");

            // Lấy danh sách giao dịch đổ vào từng dòng
            foreach (var gd in DanhSachGiaoDich)
            {
                // Mẹo: Xóa dấu phẩy (,) trong Ghi Chú để tránh lỗi nhảy cột khi mở bằng Excel
                string ghiChuAnToan = gd.GhiChu?.Replace(",", " ") ?? "";

                csv.AppendLine($"{gd.Ngay},{gd.LoaiGD},{gd.TenDanhMuc},{gd.SoTien},{ghiChuAnToan}");
            }

            // 2. Đặt tên file theo ngày xuất báo cáo hiện tại
            string tenFile = $"BaoCao_HoangPhanMoney_{DateTime.Now:dd_MM_yyyy}.csv";

            // Tạo đường dẫn lưu file tạm trong bộ nhớ đệm của điện thoại
            string duongDanFile = Path.Combine(FileSystem.CacheDirectory, tenFile);

            // Ghi file (Dùng UTF8Encoding(true) để khi mở bằng Excel không bị lỗi font Tiếng Việt)
            File.WriteAllText(duongDanFile, csv.ToString(), new UTF8Encoding(true));

            // 3. Tự động gọi menu "Chia sẻ" mặc định của điện thoại
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