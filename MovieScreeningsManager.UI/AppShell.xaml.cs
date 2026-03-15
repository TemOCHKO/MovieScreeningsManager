using MovieScreeningsManager.UI.View;

namespace MovieScreeningsManager.UI
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("CinemaHallsPage/CinemaHallDetailsPage", typeof(CinemaHallDetailsPage));
            Routing.RegisterRoute("CinemaHallDetailsPage/ScreeningDetailsPage", typeof(ScreeningDetailsPage));
        }
    }
}
