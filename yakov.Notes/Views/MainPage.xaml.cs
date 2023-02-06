using System.Collections.ObjectModel;
using yakov.Notes.ViewModel;

namespace yakov.Notes.Views;

public partial class MainPage : ContentPage
{
	public MainPage(MainPageVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}

