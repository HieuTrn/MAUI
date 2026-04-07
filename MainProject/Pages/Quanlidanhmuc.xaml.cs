using MainProject.ViewModels;

namespace MainProject.Pages;

public partial class Quanlidanhmuc : ContentPage
{
	public Quanlidanhmuc(SettingViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
    }
}