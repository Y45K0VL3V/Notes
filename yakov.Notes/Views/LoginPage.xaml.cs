using yakov.Notes.ViewModel;

namespace yakov.Notes.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage(LoginPageVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}