using Goat.Application.ViewModels;

namespace Goat.View;

public partial class MainMenuPage : ContentPage
{
	public MainMenuPage(MainMenuViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}
}