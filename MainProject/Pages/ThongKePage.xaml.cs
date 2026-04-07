using MainProject.ViewModels;

namespace MainProject.Pages;

public partial class ThongKePage : ContentPage
{
    public ThongKePage(ChartViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

    // --- THÊM HÀM NÀY VÀO ---
    protected override void OnAppearing()
    {
        base.OnAppearing();

        // Gọi lệnh làm mới dữ liệu biểu đồ mỗi khi mở trang lên
        if (BindingContext is ChartViewModel vm)
        {
            vm.LoadData();
        }
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();


        if (BindingContext is ChartViewModel vm)
        {
            vm.LoadDuLieu();
            vm.Tinhtong();
        }
    }
}