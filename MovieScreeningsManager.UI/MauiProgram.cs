
using Microsoft.Extensions.Logging;
using MovieScreeningsManager.Repositories;
using MovieScreeningsManager.Services;
using MovieScreeningsManager.Storage;
using MovieScreeningsManager.UI.View;
using MovieScreeningsManager.UI.ViewModels;

namespace MovieScreeningsManager.UI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();

#endif
            builder.Services.AddSingleton<IStorageContext, SQLLiteStorageContext>();
            builder.Services.AddSingleton<ICinemaHallRepository, CinemaHallRepository>();
            builder.Services.AddSingleton<IMovieScreeningsRepository, MovieScreeningsRepository>();

            builder.Services.AddSingleton<ICinemaHallService, CinemaHallService>();
            builder.Services.AddSingleton<IScreeningService, ScreeningService>();
            builder.Services.AddTransient<CinemaHallsPage>();
            builder.Services.AddTransient<MovieScreeningCreatePage>();
            builder.Services.AddTransient<CinemaHallEditPage>();
            builder.Services.AddTransient<MovieScreeningEditPage>();

            builder.Services.AddTransient<CinemaHallViewModel>();
            builder.Services.AddTransient<CinemaHallDetailsViewModel>();
            builder.Services.AddTransient<ScreeningDetailsViewModel>();
            builder.Services.AddTransient<ScreeningCreateViewModel>();
            builder.Services.AddTransient<ScreeningEditViewModel>();



            return builder.Build();
        }
    }

}

