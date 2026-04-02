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
            void Login()
            {
                // Implement login logic here
            }
        }