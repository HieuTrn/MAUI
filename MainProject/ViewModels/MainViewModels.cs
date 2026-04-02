using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;

namespace MainProject.ViewModels;
    public partial class MainViewModel : ObservableObject
{
    /*IConnectivity connectivity;

    public MainViewModel(IConnectivity connectivity)
    {
        this.connectivity = connectivity;
    }*/

    [RelayCommand]
    Task Navigate() => Shell.Current.GoToAsync(nameof(LoginPage));
}