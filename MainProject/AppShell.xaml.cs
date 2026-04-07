using MainProject.Pages;

namespace MainProject;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(ThongKePage), typeof(ThongKePage));
        Routing.RegisterRoute(nameof(GiaoDichPage), typeof(GiaoDichPage));
        Routing.RegisterRoute(nameof(SettingPage), typeof(SettingPage));
        Routing.RegisterRoute(nameof(ThemGiaoDichPage), typeof(ThemGiaoDichPage));
        Routing.RegisterRoute(nameof(AllTransactionsPage), typeof(AllTransactionsPage));
        Routing.RegisterRoute(nameof(QuanlitaikhoanPage), typeof(QuanlitaikhoanPage));
        Routing.RegisterRoute(nameof(Quanlidanhmuc), typeof(Quanlidanhmuc));

    }
}
