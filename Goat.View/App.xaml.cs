using Goat.Application.ViewModels;

namespace Goat.View;

public partial class App : Microsoft.Maui.Controls.Application
{
	public App(MainMenuViewModel viewModel)
	{
		InitializeComponent();

		MainPage = new MainMenuPage(viewModel);
	}
}
