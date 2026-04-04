using MainProject.Pages;

namespace MainProject;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
        Routing.RegisterRoute(nameof(ThongKePage), typeof(ThongKePage));
        Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
    }
}
