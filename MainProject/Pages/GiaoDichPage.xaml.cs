namespace MainProject.Pages;

public partial class GiaoDichPage : ContentPage
{
    public GiaoDichPage()
    {
        InitializeComponent();
    }

    // Nút FAB "+" → điều hướng sang trang Thêm Giao Dịch
    private async void OnThemGiaoDichClicked(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new ThemGiaoDichPage());
    }
}
