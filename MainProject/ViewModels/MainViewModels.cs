using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MainProject.Pages;
using MainProject.Models;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace MainProject.ViewModels;
    public partial class MainViewModel : ObservableObject
{
    [RelayCommand]
    Task Navigate() => Shell.Current.GoToAsync(nameof(LoginPage));

    [RelayCommand]
    Task Nagivate() => Shell.Current.GoToAsync(nameof(ThongKePage));
    
    [RelayCommand]
    async Task Logout()
    {
        // chưa có ý tưởng....

    }
}