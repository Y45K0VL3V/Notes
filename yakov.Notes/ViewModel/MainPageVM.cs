using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using yakov.Notes.Domain.Entities;

namespace yakov.Notes.ViewModel
{
    public partial class MainPageVM : ObservableObject
    {
        public MainPageVM()
        {
            NoteItems.Add(new() { Title = "Title text", Content = "Some text here SAGSG SADGSDF GSGS GSDF GS GSD GSA SADG ADF ASD GG SDGSDSADG DS GASGDGSADGDSAG ASDGASDSDAG WGRG WGF SFDGSFDG FDGSDF" });
            NoteItems.Add(new() { Title = "Title text", Content = "Some text here" });
            NoteItems.Add(new() { Title = "Title text", Content = "Some text here" });
            NoteItems.Add(new() { Title = "Title text", Content = "Some text here" });
            NoteItems.Add(new() { Title = "Title text", Content = "Some text here" });
            NoteItems.Add(new() { Title = "Title text", Content = "Some text here" });
        }

        [ObservableProperty]
        private int _currentIndex;

        [ObservableProperty]
        private ObservableCollection<Note> _displayNoteItems = new();

        [RelayCommand]
        private void CreateNote()
        {

        }

        [RelayCommand]
        private void OpenNote()
        {

        }

        private void OpenNoteEditor()
        {

        }
    }
}
