namespace MainProject.Pages;

public partial class GiaoDichPage : ContentPage
{
    public GiaoDichPage()
    {
        InitializeComponent();
    }
    private void OnTabClicked(object sender, TappedEventArgs e)
    {
        // Reset tất cả tab về mặc định
        var tabs = new[] { TabTatCa, TabThuNhap, TabChiTieu, TabChuyenKhoan };
        var lbls = new[] { LblTatCa, LblThuNhap, LblChiTieu, LblChuyenKhoan };

        foreach (var tab in tabs) tab.BackgroundColor = Colors.Transparent;
        foreach (var lbl in lbls) lbl.TextColor = Color.FromArgb("#64748B");

        // Bật tab được chọn
        string param = e.Parameter?.ToString();
        switch (param)
        {
            case "TatCa":
                TabTatCa.BackgroundColor = Color.FromArgb("#193CB8");
                LblTatCa.TextColor = Colors.White;
                break;
            case "ThuNhap":
                TabThuNhap.BackgroundColor = Color.FromArgb("#193CB8");
                LblThuNhap.TextColor = Colors.White;
                break;
            case "ChiTieu":
                TabChiTieu.BackgroundColor = Color.FromArgb("#193CB8");
                LblChiTieu.TextColor = Colors.White;
                break;
            case "ChuyenKhoan":
                TabChuyenKhoan.BackgroundColor = Color.FromArgb("#193CB8");
                LblChuyenKhoan.TextColor = Colors.White;
                break;
        }
    }

    // Nút FAB "+" → điều hướng sang trang Thêm Giao Dịch
    private async void OnThemGiaoDichClicked(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new ThemGiaoDichPage());
    }
}


