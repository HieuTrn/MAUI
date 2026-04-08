using MainProject.ViewModels;

namespace MainProject;

public partial class GiaoDichGanDayPage : ContentPage
{
	public GiaoDichGanDayPage(MainViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}