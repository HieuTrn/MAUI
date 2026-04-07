using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MainProject.Pages;
using MainProject.Models;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace MainProject.ViewModels;
    public partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    private string _users;

    [RelayCommand]
    async void GotoDanhMuc()
    {
        await Shell.Current.GoToAsync(nameof(DanhMucPage));
    }

    public MainViewModel()
    {
        Users = Preferences.Default.Get("ngdunght", "");
    }


}