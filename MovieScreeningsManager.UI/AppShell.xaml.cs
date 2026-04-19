using MovieScreeningsManager.UI.View;

namespace MovieScreeningsManager.UI
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute($"{nameof(CinemaHallsPage)}/{nameof(CinemaHallDetailsPage)}", typeof(CinemaHallDetailsPage));
            Routing.RegisterRoute($"{nameof(CinemaHallsPage)}/{nameof(CinemaHallDetailsPage)}/{nameof(ScreeningDetailsPage)}", typeof(ScreeningDetailsPage));
            Routing.RegisterRoute($"{nameof(CinemaHallsPage)}/{nameof(CinemaHallDetailsPage)}/{nameof(MovieScreeningCreatePage)}", typeof(MovieScreeningCreatePage));

        }
    }
}
