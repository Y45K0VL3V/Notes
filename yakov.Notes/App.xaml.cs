using yakov.Notes.Navigation;
using yakov.Notes.Views;

namespace yakov.Notes;

public partial class App : Microsoft.Maui.Controls.Application
{
	public App(INavigationService navigationService)
	{
		InitializeComponent();

		var emailTask = SecureStorage.GetAsync("yakovNotesEmail");
		emailTask.Wait();

        Current.MainPage = new NavigationPage();
        if (emailTask.Result is null)
		{
			navigationService.NavigateToLoginPage();
		}
		else
		{
			navigationService.NavigateToMainPage();
		}
    }
}
