using AppDevCW1.Data;
using Microsoft.Extensions.Logging;
using QuestPDF.Infrastructure;

namespace AppDevCW1
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
        //This code added to create QuestPDF for pdf generation
            QuestPDF.Settings.License = LicenseType.Community;
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
    		builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            //This is added to integrate Order operation
            builder.Services.AddSingleton<OrderOperation>();
            return builder.Build();
        }
    }
}
