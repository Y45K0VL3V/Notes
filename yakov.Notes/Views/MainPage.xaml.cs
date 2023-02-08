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

	private double _scrolledPoints = 0;
	private double _prevScrollPos = 0;
    private void ScrollView_Scrolled(object sender, ScrolledEventArgs e)
    {
		double diffPos = e.ScrollY - _prevScrollPos;

        if ((diffPos < 0 && _scrolledPoints > 0) ||
			(diffPos > 0 && _scrolledPoints < 0))
			_scrolledPoints = 0;
		
		_scrolledPoints += diffPos;

		if (_scrolledPoints < -40)
			addButton.IsVisible = true;

		if (_scrolledPoints > 40)
			addButton.IsVisible = false;

		_prevScrollPos = e.ScrollY;
    }
}

