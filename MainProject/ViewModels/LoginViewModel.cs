using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;


namespace MainProject.ViewModels;
public partial class LoginViewModel : ObservableObject
{

    [ObservableProperty]
    private string _username;

    [ObservableProperty]
    private string _password;

    [RelayCommand]
    async Task Login()
    {
        bool checkdn = false;
        int loggedInUserId = -1;
        if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
        {

            await Application.Current.MainPage.DisplayAlert("⚠️ Lỗi", "Vui lòng nhập đầy đủ thông tin", "✅ OK");
            return;
        }
        var dstk = App.ketnoiDB.laytk();
        for (int i = 0; i < dstk.Count; i++)
        {
            if (dstk[i].user == Username && dstk[i].password == Password)
            {
                checkdn = true;
                loggedInUserId = dstk[i].Id;
                break;
            }
        }
        if (checkdn == true)
        {
            Preferences.Default.Set("ngdunght", Username);
            Preferences.Default.Set("UserID", loggedInUserId);
            Application.Current.MainPage = new AppShell();
        }
        else
        {
            await Application.Current.MainPage.DisplayAlert("⚠️ Lỗi", "Sai tên đăng nhập hoặc mật khẩu!", "✅ OK");
        }
    }
    [RelayCommand]
    async Task Dangki()
    {
        var cbichuyen = Application.Current.Handler.MauiContext.Services;
        var taodgki = cbichuyen.GetService<Pages.RegisterPage>();
        await Application.Current.MainPage.Navigation.PushAsync(taodgki);
    }
}