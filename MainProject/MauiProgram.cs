using LiveChartsCore.SkiaSharpView.Maui;
using MainProject.Pages;
using MainProject.ViewModels;
using SkiaSharp.Views.Maui.Controls.Hosting;

namespace MainProject;

    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseSkiaSharp()
                .UseLiveCharts()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<MainViewModel>();
            builder.Services.AddTransient<DanhMucViewModel>();
            builder.Services.AddTransient<DanhMucPage>();



            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<ThongKePage>();
            builder.Services.AddTransient<MainViewModel>();
            builder.Services.AddTransient<RegisterPage>();
            builder.Services.AddTransient<RegisterViewModel>();

            return builder.Build();
        }
    }