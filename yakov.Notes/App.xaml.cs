using yakov.Notes.Navigation;
using yakov.Notes.Views;

namespace yakov.Notes;

public partial class App : Application
{
	public App(INavigationService navigationService)
	{
		InitializeComponent();

		Current.MainPage = new NavigationPage();
		navigationService.NavigateToLoginPage();
    }
}
