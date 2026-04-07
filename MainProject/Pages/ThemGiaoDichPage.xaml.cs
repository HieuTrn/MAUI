using MainProject.Database;
using MainProject.Models;
using Microsoft.Maui.Graphics;
using System.Collections.ObjectModel;

namespace MainProject.Pages;

public partial class ThemGiaoDichPage : ContentPage
{
    private string _loaiGiaoDich = "Chi Tiêu";
    private int? _idDanhMucDuocChon = null;
    private KetnoiDB _db;

    public ThemGiaoDichPage()
    {
        InitializeComponent();
        _db = new KetnoiDB();
        PickerNgay.Date = DateTime.Now;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadDanhCach();
    }

    private void LoadDanhCach()
    {
        try
        {
            _db.Taodb();
            var dsDb = _db.layDanhmuc();
            var dsHienThi = new ObservableCollection<danhmuc>(); // Sử dụng trực tiếp model danhmuc

            foreach (var dm in dsDb)
            {
                // Chỉ hiển thị danh mục thuộc đúng Loại Giao Dịch đang chọn
                if (dm.LoaiGD == _loaiGiaoDich)
                {
                    dsHienThi.Add(dm);
                }
            }

            CvDanhCach.ItemsSource = dsHienThi;
            _idDanhMucDuocChon = null; // Bỏ chọn khi load lại list
        }
        catch
        {
            CvDanhCach.ItemsSource = new ObservableCollection<danhmuc>();
        }
    }

    private async void OnDongClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }

    private void OnChiTieuTapped(object sender, EventArgs e)
    {
        _loaiGiaoDich = "Chi Tiêu";
        BtnChiTieu.BackgroundColor = Color.FromArgb("#FFA94D");
        LblChiTieu.TextColor = Colors.White;
        BtnThuNhap.BackgroundColor = Color.FromArgb("#DDE3F0");
        LblThuNhap.TextColor = Color.FromArgb("#64748B");
        EntrySoTien.TextColor = Color.FromArgb("#FF6467");

        LoadDanhCach();
    }

    private void OnThuNhapTapped(object sender, EventArgs e)
    {
        _loaiGiaoDich = "Thu Nhập";
        BtnThuNhap.BackgroundColor = Color.FromArgb("#38D5BE");
        LblThuNhap.TextColor = Colors.White;
        BtnChiTieu.BackgroundColor = Color.FromArgb("#DDE3F0");
        LblChiTieu.TextColor = Color.FromArgb("#64748B");
        EntrySoTien.TextColor = Color.FromArgb("#38D5BE");

        LoadDanhCach();
    }

    private void OnDanhMucSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is danhmuc dmChon)
        {
            _idDanhMucDuocChon = dmChon.Id;
        }
    }

    private async void OnLuuClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(EntrySoTien.Text) ||
            !double.TryParse(EntrySoTien.Text, out double soTien) ||
            soTien <= 0)
        {
            await DisplayAlert("Lỗi", "Vui lòng nhập số tiền hợp lệ!", "OK");
            return;
        }

        if (_idDanhMucDuocChon == null)
        {
            await DisplayAlert("Lỗi", "Vui lòng chọn một danh mục!", "OK");
            return;
        }

        try
        {
            DateTime ngay = (DateTime)PickerNgay.Date;
            string ghiChu = EditorGhiChu.Text ?? "";
            int idTaiKhoan = Preferences.Default.Get("UserID", 1);

            _db.ThemGd(soTien, _loaiGiaoDich, idTaiKhoan, _idDanhMucDuocChon.Value, ngay, ghiChu);

            await DisplayAlert("Thành công", "Đã lưu giao dịch!", "OK");
            await Shell.Current.GoToAsync("..");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Lỗi Database", $"Không thể lưu giao dịch:\n{ex.Message}", "OK");
        }
    }
}