using MainProject.ViewModels;

namespace MainProject.Pages;

public partial class MainPage : ContentPage
{
    public MainPage(MainViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
    private async void OnViewAllTapped(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AllTransactionsPage());
    }
}