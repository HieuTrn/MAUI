using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MainProject.Models;
using MainProject.Pages;
using System.Collections.ObjectModel;

namespace MainProject.ViewModels
{
    public partial class SettingViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _user;

        public SettingViewModel()
        {
            User = Preferences.Default.Get("ngdunght", "");
        }

        [RelayCommand]
        async Task Dangxuat()
        {

            bool ok = await Application.Current.MainPage.DisplayAlert(" ⚠️ Thông báo", "Bạn có chắc muốn đăng xuất không?", "✅ OK", "❌ Hủy");

            if (ok)
            {
                Preferences.Default.Remove("ngdunght");
                Application.Current.MainPage = new NavigationPage(new LoginPage(new LoginViewModel()));
            }
        }
        [RelayCommand]
        async Task Moqltk() {
            await Shell.Current.GoToAsync(nameof(QuanlitaikhoanPage));
        }
        [RelayCommand]
        async Task Moqldm()
        {
            await Shell.Current.GoToAsync(nameof(Quanlidanhmuc));
        }
        [RelayCommand]
        async Task themtk()
        {
            await Application.Current.MainPage.DisplayAlert(" ⚠️ Thông báo", "Chức năng đang phát triển", "✅ OK");
        }

    }
}