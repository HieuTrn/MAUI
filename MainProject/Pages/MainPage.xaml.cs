using MainProject.ViewModels;
namespace MainProject.Pages;

public partial class MainPage : ContentPage
{
    public MainPage(MainViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
        BindingContext = new ChartViewModel();
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();

        
        if (BindingContext is MainViewModel vm)
        {
            vm.LoadDuLieu();
            vm.Tinhtong();
            
            
        }
    }
    private async void OnViewAllTapped(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(GiaoDichGanDayPage));
    }
}