using MovieScreeningsManager.UI.ViewModels;

namespace MovieScreeningsManager.UI.View;

public partial class MovieScreeningCreatePage : ContentPage
{
	public MovieScreeningCreatePage(ScreeningCreateViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
    }
}