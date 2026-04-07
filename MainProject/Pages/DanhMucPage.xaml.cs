namespace MainProject.Pages;

public partial class DanhMucPage : ContentPage
{
	public DanhMucPage()
	{
		InitializeComponent();
		this.BindingContext = new ViewModels.DanhMucViewModel();
    }
}