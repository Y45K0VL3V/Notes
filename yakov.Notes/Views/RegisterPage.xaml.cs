using yakov.Notes.ViewModel;

namespace yakov.Notes.Views;

public partial class RegisterPage : ContentPage
{
	public RegisterPage(RegisterPageVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}