using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace MainProject.ViewModels
{
    public partial class RegisterViewModel : MainViewModel
    {
        [ObservableProperty]
        private string _username;
        [ObservableProperty]
        private string _password;
        [ObservableProperty]
        private string _cPassword;



        [RelayCommand]
        async Task Taodki()
        {

            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password) || string.IsNullOrWhiteSpace(CPassword))
            {
                await Application.Current.MainPage.DisplayAlert("Lỗi⛔", "Vui lòng nhập đầy đủ thông tin", "OK✅");
                return;
            }
            if (Password != CPassword)
            {
                await Application.Current.MainPage.DisplayAlert("Lỗi⛔", "Mật khẩu không khớp, vui lòng nhập lại!", "OK✅");
                return; 
            }

            var dstk = App.ketnoiDB.laytk();
            bool sameten = false;

            for (int i = 0; i < dstk.Count; i++)
            {
                if (dstk[i].user == Username)
                {
                    sameten = true;
                    break;
                }
            }

            if (sameten == true)
            {
                await Application.Current.MainPage.DisplayAlert("Lỗi⛔", "Tên đăng nhập này đã có . Vui lòng chọn tên khác!", "OK✅");
                return;
            }

            App.ketnoiDB.themTK(Username, Password);
            await Application.Current.MainPage.DisplayAlert("Thành công✅", "Đăng ký tài khoản thành công!", "OK✅");

            Username = string.Empty;
            Password = string.Empty;
            CPassword = string.Empty;

            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
    }
