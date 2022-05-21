using Goat.Application.ViewModels;

namespace Goat.View.Pages;

public partial class GameSettingsPage : ContentPage
{
	public GameSettingsPage(GameSettingsViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}
}