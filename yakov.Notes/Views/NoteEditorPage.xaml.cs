using yakov.Notes.ViewModel;

namespace yakov.Notes.Views;

public partial class NoteEditorPage : ContentPage
{
	public NoteEditorPage(NoteEditorPageVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}