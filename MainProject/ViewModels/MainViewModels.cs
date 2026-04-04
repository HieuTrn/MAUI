using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MainProject.Pages;
using MainProject.Model;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace MainProject.ViewModels;
    public partial class MainViewModel : ObservableObject
{
    public ObservableCollection<ThuocTinh> ThuocTinhs { get; set; } = new ();

    public MainViewModel()
    {
        ThuocTinhs.Add(new ThuocTinh { NoiDung = "Ăn uống"});
        ThuocTinhs.Add(new ThuocTinh { NoiDung = "Mua sắm"});
        ThuocTinhs.Add(new ThuocTinh { NoiDung = "Giải trí"});
        ThuocTinhs.Add(new ThuocTinh { NoiDung = "Hóa Đơn"});
    }

    [RelayCommand]
    Task Navigate() => Shell.Current.GoToAsync(nameof(LoginPage));

    [RelayCommand]
    Task Nagivate() => Shell.Current.GoToAsync(nameof(ThongKePage));
}