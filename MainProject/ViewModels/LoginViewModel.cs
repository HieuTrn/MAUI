using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;


namespace MainProject.ViewModels;
public partial class LoginViewModel : MainViewModel
{

    [ObservableProperty]
    private string _username;

    [ObservableProperty]
    private string _password;

    [RelayCommand]
    async Task Login()
    {
        bool checkdn = false;
        if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
        {

            await Application.Current.MainPage.DisplayAlert("⚠️ Lỗi", "Vui lòng nhập đầy đủ thông tin", "OK");
            return;
        }
        var dstk = App.ketnoiDB.laytk();
        for (int i = 0; i < dstk.Count; i++)
        {
            if (dstk[i].user == Username && dstk[i].password == Password)
            {
                checkdn = true;
                break;
            }
        }
        if (checkdn == true)
        {
            Preferences.Default.Set("loginchua", true);
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