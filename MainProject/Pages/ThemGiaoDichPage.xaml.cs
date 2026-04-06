namespace MainProject.Pages;

public partial class ThemGiaoDichPage : ContentPage
{
    public ThemGiaoDichPage()
    {
        InitializeComponent();
    }

    // Nút X → đóng trang, quay lại GiaoDichPage
    private async void OnDongClicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }

    // Nút Lưu → TODO: lưu giao dịch vào DB rồi đóng
    private async void OnLuuClicked(object sender, EventArgs e)
    {
        await DisplayAlert("Thông báo", "Chức năng lưu đang phát triển!", "OK");
        await Navigation.PopModalAsync();
    }

    private void OnLoaiGDClicked(object sender, TappedEventArgs e)
{
    bool isChiTieu = e.Parameter?.ToString() == "ChiTieu";
    BtnChiTieu.BackgroundColor = isChiTieu ? Color.FromArgb("#FFA94D") : Color.FromArgb("#DDE3F0");
    BtnThuNhap.BackgroundColor = isChiTieu ? Color.FromArgb("#DDE3F0") : Color.FromArgb("#FFA94D");
}
}
