using yakov.Notes.Views;

namespace yakov.Notes;

public partial class App : Application
{
	public App(LoginPage startPage)
	{
		InitializeComponent();

		MainPage = new NavigationPage(startPage);
	}
}
