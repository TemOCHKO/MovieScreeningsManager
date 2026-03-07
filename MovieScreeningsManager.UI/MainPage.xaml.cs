using MovieScreeningsManager.Services;
using MovieScreeningsManager.UI.ViewModel;

namespace MovieScreeningsManager.UI
{

    public partial class MainPage : ContentPage
    {
        private static StorageService _storageService;
        int count = 0;

        public MainPage(CinemaHallViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}
