using MainProject.ViewModels;

namespace MainProject;

public partial class LoginPage : ContentPage
{
	public LoginPage(MainViewModel vm)
	{
        InitializeComponent();
        BindingContext = vm;
    }
}