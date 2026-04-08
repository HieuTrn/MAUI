using MainProject.ViewModels;

namespace MainProject.Pages;

public partial class ThongKePage : ContentPage
{
    public ThongKePage(ChartViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
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