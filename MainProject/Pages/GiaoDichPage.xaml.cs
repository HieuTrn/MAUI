using MainProject.ViewModels;

namespace MainProject.Pages;

public partial class GiaoDichPage : ContentPage
{
    public GiaoDichPage(MainViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

    
    private async void OnThemGiaoDichClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(ThemGiaoDichPage));
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();


        if (BindingContext is ChartViewModel vm)
        {
            vm.LoadData();
            vm.Tinhtong();
        }
    }
}
