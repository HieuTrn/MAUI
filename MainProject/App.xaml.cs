using MainProject.Pages;
using MainProject.Database;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using MainProject.ViewModels;


namespace MainProject
{
    public partial class App : Application
    {
        public static readonly KetnoiDB ketnoiDB= new KetnoiDB();
        public App()
        {
            InitializeComponent();

            bool ktralogin = Preferences.Get("loginchua", false);
            if(ktralogin == true) {
                MainPage = new AppShell();
            }
            else
            {
                    MainPage = new NavigationPage(new LoginPage(new LoginViewModel()));
            }
        }
        protected override void OnStart()
        {
            base.OnStart();

            try
            {
                ketnoiDB.Taodb();
                
            }
            catch (Exception ex)
            {
                Debug.WriteLine("❌ Lỗi: " + ex.ToString());
            }
        }
    }
}