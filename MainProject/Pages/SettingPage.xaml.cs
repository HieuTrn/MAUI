using MainProject.ViewModels;

namespace MainProject.Pages;

public partial class SettingPage : ContentPage
{
	public SettingPage(SettingViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;

	}
}