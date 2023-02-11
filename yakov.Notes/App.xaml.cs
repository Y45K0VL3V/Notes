using yakov.Notes.Navigation;
using yakov.Notes.Views;

namespace yakov.Notes;

public partial class App : Microsoft.Maui.Controls.Application
{
	public App(INavigationService navigationService)
	{
		InitializeComponent();

		Current.MainPage = new NavigationPage();
		navigationService.NavigateToLoginPage();
    }
}
