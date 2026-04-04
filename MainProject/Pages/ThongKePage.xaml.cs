using MainProject.ViewModels;

namespace MainProject.Pages;

public partial class ThongKePage : ContentPage
{
	public ThongKePage(MainViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
    }
}