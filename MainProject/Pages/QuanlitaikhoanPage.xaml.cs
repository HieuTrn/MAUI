using MainProject.ViewModels;

namespace MainProject.Pages;

public partial class QuanlitaikhoanPage : ContentPage
{
	public QuanlitaikhoanPage(SettingViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}