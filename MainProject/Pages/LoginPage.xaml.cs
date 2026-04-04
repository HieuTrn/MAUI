using MainProject.ViewModels;

namespace MainProject.Pages;

public partial class LoginPage : ContentPage
{
	public LoginPage(MainViewModel vm)
	{
        InitializeComponent();
        BindingContext = vm;
    }
}