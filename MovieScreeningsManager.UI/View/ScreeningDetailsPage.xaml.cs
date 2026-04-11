using MovieScreeningsManager.Services;
using MovieScreeningsManager.UI.ViewModels;
namespace MovieScreeningsManager.UI.View;

public partial class ScreeningDetailsPage : ContentPage
{
   
   
    public ScreeningDetailsPage(ScreeningDetailsViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}