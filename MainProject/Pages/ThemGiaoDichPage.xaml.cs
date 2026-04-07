namespace MainProject.Pages;

public partial class ThemGiaoDichPage : ContentPage
{
    public ThemGiaoDichPage()
    {
        InitializeComponent();
        this.BindingContext = new ViewModels.DanhMucViewModel();
    }

    // Nút X → đóng trang, quay lại GiaoDichPage
    private async void OnDongClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }

    // Nút Lưu → TODO: lưu giao dịch vào DB rồi đóng
    private async void OnLuuClicked(object sender, EventArgs e)
    {
        await DisplayAlert("Thông báo", "Chức năng lưu đang phát triển!", "OK");
        await Shell.Current.GoToAsync("..");
    }
}
